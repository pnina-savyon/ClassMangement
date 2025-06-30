using Google.OrTools.Sat;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Rules
{
    public class FavoriteFriendsInNearbySeat : IScoringRule
    {
		public int CalculateActualScore(Student student, int assignedChairId, StudentContext context, CpSolver solver)
		{
			int score = (student.StatusSocial == Levels.E || student.StatusSocial == Levels.D ? 15 : 13)
						+ (student.Priority ?? 0);
			int totalScore = 0;

			Chair? assignedChair = context.Chairs.FirstOrDefault(c => c.Id == assignedChairId);
			if (assignedChair == null) return 0;

			foreach (Chair nearby in assignedChair.NearbyChairs ?? new List<Chair>())
			{
				foreach (Student friend in student.FavoriteFriends ?? new List<Student>())
				{
					if (!context.StudentChairVars.ContainsKey(friend.Id)) continue;

					int friendChairId = (int)solver.Value(context.StudentChairVars[friend.Id]);
					if (friendChairId == nearby.Id)
						totalScore += score;
				}
			}

			return totalScore;
		}



		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
        {
            int score = (student.StatusSocial == Levels.E ||
                student.StatusSocial == Levels.D ? 15 : 13) + (student.Priority ?? 1);
            
            List<LinearExpr> terms = new List<LinearExpr>();
            foreach (Chair chair in context.Chairs)
            {
                foreach (Chair nearByChair in chair.NearbyChairs ?? new List<Chair>())
                {
                    foreach (Student favoriteFriend in student.FavoriteFriends ?? new List<Student>())
                    {
                        BoolVar both = context.Model.NewBoolVar($"student_{student.Id}_with_friend_{favoriteFriend.Id}_near_{nearByChair.Id}");
                        context.Model.Add(context.StudentChairVars[student.Id] == chair.Id).OnlyEnforceIf(both);
                        context.Model.Add(context.StudentChairVars[favoriteFriend.Id] == nearByChair.Id).OnlyEnforceIf(both);
                        //context.Objective.AddTerm(both, score);
                        terms.Add(both*score);    
                    }
                }
            }
            return LinearExpr.Sum(terms);
        }
    }

}

