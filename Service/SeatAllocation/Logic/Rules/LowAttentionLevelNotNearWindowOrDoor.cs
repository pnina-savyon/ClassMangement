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
	public class LowAttentionLevelNotNearWindowOrDoor : IScoringRule
	{

		public int CalculateActualScore(Student student, int assignedChairId, StudentContext context, CpSolver solver)
		{
			if (student.AttentionLevel != Levels.E && student.AttentionLevel != Levels.D)
				return 0;

			int score = student.AttentionLevel == Levels.E ? -4 : -3;
			score += student.Priority ?? 1;

			Chair? assignedChair = context.Chairs.FirstOrDefault(c => c.Id == assignedChairId);
			if (assignedChair == null)
				return 0;

			return (assignedChair.IsNearTheDoor || assignedChair.IsNearTheWindow) ? score : 0;
		}


		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
		{
			if (student.AttentionLevel != Levels.E && student.AttentionLevel != Levels.D)
				return LinearExpr.Constant(0);

			int score = student.AttentionLevel == Levels.E ? -4 : -3;
			score += student.Priority ?? 1;

			List<LinearExpr> terms = new List<LinearExpr>();

			foreach (Chair chair in context.Chairs)
			{
				if (chair.IsNearTheDoor || chair.IsNearTheWindow)
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
