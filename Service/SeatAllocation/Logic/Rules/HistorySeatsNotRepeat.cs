﻿using Google.OrTools.Sat;
using Repository.Entities;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Rules
{
    public class HistorySeatsNotRepeat : IScoringRule
    {
		public int CalculateActualScore(Student student, Chair assignedChair, StudentContext context, CpSolver solver)
		{
            //
            if (assignedChair == null)
                return 0;
			if (student.ChairId != null && student.ChairId == assignedChair?.Id)
			{
				return -7 + (student.Priority ?? 0);
			}

			if (student.HistoryChairs != null && student.HistoryChairs.Contains(assignedChair.Id))
			{
				return -4 + (student.Priority ?? 0);
			}

			return 0;
		}



		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
        {
            //current
            int score = 0;
            List<LinearExpr> terms = new List<LinearExpr>();

            foreach (Chair chair in context.Chairs)
            {
                if (student.ChairId == chair.Id)
                {
                    score = -7 + (student.Priority ?? 1);

                    BoolVar isMatch = context.Model.NewBoolVar($"student_{student.Id}_chair_{chair.Id}");
                    context.Model.Add(studentChairVar == chair.Id).OnlyEnforceIf(isMatch);
                    context.Model.Add(studentChairVar != chair.Id).OnlyEnforceIf(isMatch.Not());
                    terms.Add(isMatch * score);
                }
                else
                {
                    score = -4 + (student.Priority ?? 1);
                    foreach (int chairHistoryId in student.HistoryChairs ?? new List<int>())
                    {
                        if (chairHistoryId == chair.Id)
                        {
                            BoolVar isMatch = context.Model.NewBoolVar($"student_{student.Id}_chair_{chair.Id}");
                            context.Model.Add(studentChairVar == chair.Id).OnlyEnforceIf(isMatch);
                            context.Model.Add(studentChairVar != chair.Id).OnlyEnforceIf(isMatch.Not());
                            terms.Add(isMatch * score);
                        }
                    }
                }
            }
            return LinearExpr.Sum(terms);
        }
    }
}
