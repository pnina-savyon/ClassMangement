using Google.OrTools.Sat;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Rules
{
    public class NonFavoriteFriendsNotNearBySeat : IScoringRule
    {
		public int CalculateActualScore(Student student, Chair assignedChair, StudentContext context, CpSolver solver)
		{
			int score = (student.AttentionLevel == Levels.E || student.AttentionLevel == Levels.D ? -16 : -14) + (student.Priority ?? 1);
			int totalScore = 0;

			if (assignedChair == null)
				return 0;

			foreach (Chair nearby in assignedChair.NearbyChairs ?? new List<Chair>())
			{
				foreach (Student nonFavorite in student.NonFavoriteFriends ?? new List<Student>())
				{
					if (!context.StudentChairVars.ContainsKey(nonFavorite.Id))
						continue;

					int friendChairId = (int)solver.Value(context.StudentChairVars[nonFavorite.Id]);
					if (friendChairId == nearby.Id)
						totalScore += score;
				}
			}

			return totalScore;
		}



		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
        {
            int score = (student.AttentionLevel == Levels.E ||
                student.AttentionLevel == Levels.D ? -16 : -14) + (student.Priority ?? 1);
            
            List<LinearExpr> terms = new List<LinearExpr>();
            foreach (Chair chair in context.Chairs)
            {
                foreach (Chair nearByChair in chair.NearbyChairs ?? new List<Chair>())
                {
                    foreach (Student nonFavoriteFriend in student.NonFavoriteFriends ?? new List<Student>())
                    {
                        BoolVar both = context.Model.NewBoolVar($"student_{student.Id}_with_friend_{nonFavoriteFriend.Id}_near_{nearByChair.Id}");
                        context.Model.Add(context.StudentChairVars[student.Id] == chair.Id).OnlyEnforceIf(both);
                        context.Model.Add(context.StudentChairVars[nonFavoriteFriend.Id] == nearByChair.Id).OnlyEnforceIf(both);
                        //context.Objective.AddTerm(both, score);
                        terms.Add(both*score);
                    }
                }
            }
            //
            return LinearExpr.Sum(terms);
        }
    }
}
