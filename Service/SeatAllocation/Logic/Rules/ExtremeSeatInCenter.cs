using Google.OrTools.Sat;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Rules
{
    public class ExtremeSeatInCenter : IScoringRule
    {
        public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
        {
            //האם להוסיף אפשרות של 0?

            //יש מרכזי יש לא מרכזי ויש קיצוני - אז להחליט את זה כאן?
            //int?
            int score = (student.CurrentChair.IsCenteral ? 10 : -3) * (int)(student.Priority);

            List<LinearExpr> terms = new List<LinearExpr>();      
            foreach (Chair chair in context.Chairs)
            {
                if (!chair.IsCenteral)
                {
                    BoolVar isMatch = context.Model.NewBoolVar($"student_{student.Id}_chair_{chair.Id}");
                    context.Model.Add(studentChairVar == chair.Id).OnlyEnforceIf(isMatch);
                    context.Model.Add(studentChairVar != chair.Id).OnlyEnforceIf(isMatch.Not());
                    
                    terms.Add(isMatch * score);
                }
            }
            return LinearExpr.Sum(terms);
        }
    }
    
}
