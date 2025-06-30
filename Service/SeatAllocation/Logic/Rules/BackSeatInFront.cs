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
using static System.Formats.Asn1.AsnWriter;

namespace Service.SeatAllocation.Logic.Rules
{
	public class BackSeatInFront : IScoringRule
	{
		public int CalculateActualScore(Student student, Chair assignedChair, StudentContext context, CpSolver solver)
		{
			if (student.CurrentChair == null)
				return 0;

			if (assignedChair == null)
				return 0;

			int score = student.CurrentChair.IsFront ? -3 : 11;
			if (student.Priority != null)
				score += student.Priority.Value;

			if (assignedChair.IsFront)
				return score;

			return 0;
		}


		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
		{
			if (student.CurrentChair == null)
				return LinearExpr.Constant(0);

			int score = student.CurrentChair.IsFront? (student.Priority ?? 1) + -3: (student.Priority ?? 1) + 11;
			List<LinearExpr> terms = new List<LinearExpr>();

            foreach (Chair chair in context.Chairs)
			{
				if (chair.IsFront)
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
