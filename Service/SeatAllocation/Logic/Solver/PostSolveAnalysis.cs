using Common.Dto;
using Google.OrTools.Sat;
using Repository.Entities;
using Service.SeatAllocation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Solver
{
    public class PostSolveAnalysis
    {

		private Dictionary<string, int> actualScores;
		private Dictionary<string, int> maxScores;
		private int totalScoreAllStudents;

		public Dictionary<string, int> ComputeActualScores(List<Student> students, StudentContext context, CpSolver solver, List<IScoringRule> scoringRules)
		{
			var actualScores = new Dictionary<string, int>();

			foreach (Student student in students)
			{
				if (!context.StudentChairVars.ContainsKey(student.Id)) continue;

				long assignedChairId = solver.Value(context.StudentChairVars[student.Id]);
				int score = 0;

				foreach (IScoringRule rule in scoringRules)
				{
					score += rule.CalculateActualScore(student, assignedChairId, context, solver);
				}

				actualScores[student.Id] = score;
			}

			return actualScores;
		}

		public Dictionary<string, int> ComputeMaxScores(List<Student> students, StudentContext context, List<IScoringRule> scoringRules)
		{
			var maxScores = new Dictionary<string, int>();

			foreach (Student student in students)
			{
				int maxScore = 0;
				foreach (Chair chair in context.Chairs)
				{
					int score = 0;
					foreach (IScoringRule rule in scoringRules)
					{
						score += rule.CalculateActualScore(student, chair.Id, context, null);
					}
					if (score > maxScore)
						maxScore = score;
				}
				maxScores[student.Id] = maxScore;
			}

			return maxScores;
		}

		public int ComputeTotalScoreAllStudents(Dictionary<string, int> actualScores)
		{
			// לבדוק גם את פרמטר הניקוד הסופי
			return actualScores.Values.Sum();
		}

		public int ComputeNewPriority(int actualScore, int maxScoreForStudent, int totalScoreAllStudents, int previousPriority)
		{
			double personalRatio = actualScore / (maxScoreForStudent == 0 ? 1.0 : maxScoreForStudent);
			double globalRatio = actualScore / (totalScoreAllStudents == 0 ? 1.0 : totalScoreAllStudents);
			double combinedScore = 0.5 * personalRatio + 0.5 * globalRatio;

			int adjustment = (int)Math.Round((1.0 - combinedScore) * 10);
			int newPriority = adjustment + (int)Math.Round(previousPriority * (combinedScore < 0.5 ? 1.0 : 0.3));

			return Math.Clamp(newPriority, 1, 10);
		}

		public void UpdateAllStudentPriorities(List<Student> students, Dictionary<string, int> actualScores, Dictionary<string, int> maxScores, int totalScoreAllStudents)
		{
			// לעשות פונקציה של עידכון ברפוזיטורי
			foreach (Student student in students)
			{
				int actual = actualScores.ContainsKey(student.Id) ? actualScores[student.Id] : 0;
				int max = maxScores.ContainsKey(student.Id) ? maxScores[student.Id] : 1;
				int prev = student.Priority ?? 5;

				int newPriority = ComputeNewPriority(actual, max, totalScoreAllStudents, prev);
				student.Priority = newPriority;
			}
		}

		public void APICalculatePriority()
		{
			//זימון כל הפונקציות

		}


	}
}
