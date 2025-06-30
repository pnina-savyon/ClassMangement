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
		public int CalculateActualScore(Student student, Chair assignedChair, StudentContext context, CpSolver solver)
		{
			// תנאי סף – רק תלמידים עם רמת קשב נמוכה ורמת מוסר גבוהה
			bool isLowAttention = student.AttentionLevel == Levels.E || student.AttentionLevel == Levels.D;
			bool isHighMoral = student.MoralLevel == Levels.A || student.MoralLevel == Levels.B;

			if (!isLowAttention && !isHighMoral)
				return 0;

			int score = 0;

			// ניקוד לפי רמת קשב
			if(isLowAttention)
				score += student.AttentionLevel == Levels.E ? 10 : 8;

			// ניקוד לפי רמת מוסר
			if(isHighMoral)
				score += student.MoralLevel == Levels.A ? 14 : 12;

			// הוספת עדיפות
			score += student.Priority ?? 0;

			// בדיקה אם הכיסא המוקצה הוא מקדימה
			if (assignedChair == null || !assignedChair.IsFront && !assignedChair.IsCenteral)
				return 0;
			return score;
		}

		public LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context)
		{
			if ((student.AttentionLevel != Levels.E && student.AttentionLevel != Levels.D)
				&& (student.MoralLevel == Levels.E || student.MoralLevel == Levels.D || student.MoralLevel == Levels.C))
				return LinearExpr.Constant(0);

			int score = 0;
			// AttentionLevel = E  - 10
			// AttentionLevel = D - 8
			// MoralLevel = A - 14
			// MoralLevel = B - 12

			score += student.AttentionLevel == Levels.E ? 10 : student.AttentionLevel == Levels.D ? 8 :0;
			score += student.MoralLevel == Levels.A ? 14 : student.MoralLevel == Levels.B ? 12 : 0;
			score += student.Priority ?? 1;


			List<LinearExpr> terms = new List<LinearExpr>();

			foreach (Chair chair in context.Chairs)
			{
				if (chair.IsFront || chair.IsCenteral)
				{
					BoolVar isMatch = context.Model.NewBoolVar($"student_{student.Id}_chair_{chair.Id}");
					context.Model.Add(studentChairVar == chair.Id).OnlyEnforceIf(isMatch);
					context.Model.Add(studentChairVar != chair.Id).OnlyEnforceIf(isMatch.Not());


					if (chair.IsFront)
						terms.Add(isMatch * score);
					if (chair.IsCenteral)
						terms.Add(isMatch * score);	
				}
			}
			return LinearExpr.Sum(terms);
		}
	}
}
