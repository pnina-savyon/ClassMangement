using Repository.Entities;
using Repository.Entities.Enums;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.Sat;


namespace Service.SeatAllocation.Logic.Rules
{
	public class LowAttentionLevelInCenter : IScoringRule
	{
		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
		{
			//priority
			if ((student.AttentionLevel != Levels.E && student.AttentionLevel != Levels.D)
				|| (student.MoralLevel == Levels.E || student.MoralLevel == Levels.D))
				return LinearExpr.Constant(0);
			//למורל לא מינוס?
			//== or !=?

			int score = student.AttentionLevel == Levels.E ? 10 : 8;
			//?? וכן האם -5 וכן האם בכלל ההגדרה יפה ונכונה?
			score = student.MoralLevel == Levels.E || student.MoralLevel == Levels.D ? -5 : score;

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
