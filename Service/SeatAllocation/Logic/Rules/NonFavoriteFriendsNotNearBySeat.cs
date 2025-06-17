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
        public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
        {
            int score = (student.AttentionLevel == Levels.E ||
                student.AttentionLevel == Levels.D ? -16 : -14) * (student.Priority ?? 1);

            foreach (Chair chair in context.Chairs)
            {
                foreach (Chair nearByChair in chair.NearbyChairs)
                {
                    //student.NonFavoriteFriends ?? new List<Student>()
                    foreach (Student nonFavoriteFriend in student.NonFavoriteFriends)
                    {
                        BoolVar both = context.Model.NewBoolVar($"student_{student.Id}_with_friend_{nonFavoriteFriend.Id}_near_{nearByChair.Id}");
                        context.Model.Add(context.StudentChairVars[student.Id] == chair.Id).OnlyEnforceIf(both);
                        context.Model.Add(context.StudentChairVars[nonFavoriteFriend.Id] == nearByChair.Id).OnlyEnforceIf(both);
                        context.Objective.AddTerm(both, score);
                    }
                }
            }
            //
            return LinearExpr.Constant(0);
        }
    }
}
