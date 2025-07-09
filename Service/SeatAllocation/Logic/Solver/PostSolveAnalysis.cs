using Common.Dto;
using Google.OrTools.Sat;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Interfaces;
using Service.SeatAllocation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace Service.SeatAllocation.Logic.Solver
{
    public class PostSolveAnalysis: IPostSolverAnalysis
	{
		// אפשר להעביר את הפרמטרים של הפונקציות לחברי מחלקה
		private Dictionary<string, int> priorities;
		private Dictionary<string, int> actualScores;
		private Dictionary<string, int> maxScores;
		private Dictionary<string, int> chairOfStudent;
		private List<Student> students;
		private List<Chair> chairs;
		private List<IScoringRule> scoringRules;
		private readonly ILogger<Solver> _logger;
		private readonly IRepository<Student, string> studentRepository;
		private readonly IRepository<Chair, int> chairRepository;
		private StudentContext context;
		private CpSolver solver;
		private int totalScoreAllStudents;

		public PostSolveAnalysis(ILogger<Solver> logger, IRepository<Student, string> studentRepository, IRepository<Chair, int> chairRepository)
		{
			_logger = logger;
			this.studentRepository = studentRepository;
			this.chairRepository = chairRepository;
			
		}
		//clculate לחסוך קריאה פעמיים!!!
		public async Task<Dictionary<string, int>> ComputeActualScores()
		{
			Dictionary<string, int> actualScores = new Dictionary<string, int>();

			foreach (Student student in students)
			{
				if (!context.StudentChairVars.ContainsKey(student.Id)) continue;

				int assignedChairId = (int)solver.Value(context.StudentChairVars[student.Id]);
				Chair assignedChair = await chairRepository.GetById(assignedChairId);
				int score = 0;

				foreach (IScoringRule rule in scoringRules)
				{
					score += rule.CalculateActualScore(student, assignedChair, context, solver);
				}

				actualScores[student.Id] = score;
                _logger.LogInformation($"===========scores: {score}");

            }

            return actualScores;
		}

		public Dictionary<string, int> ComputeMaxScores()
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
						//solver
						score += rule.CalculateActualScore(student, chair, context, solver);

                    }
                    if (score > maxScore)
						maxScore = score;
				}
				maxScores[student.Id] = maxScore;

            }

            return maxScores;
		}

		public int ComputeTotalScoreAllStudents()
		{
			// לבדוק גם את פרמטר הניקוד הסופי


			int compare = solver.ObjectiveValue.CompareTo(actualScores.Values.Sum());
			_logger.LogInformation(compare.ToString());

			return actualScores.Values.Sum();
		}

		public int ComputeNewPriority(int actualScore, int maxScoreForStudent, int totalScoreAllStudents, int previousPriority)
		{
			double personalRatio = actualScore / (maxScoreForStudent == 0 ? 1.0 : maxScoreForStudent);
			double globalRatio = actualScore / (totalScoreAllStudents == 0 ? 1.0 : totalScoreAllStudents);
			double combinedScore = 0.6 * personalRatio + 0.4 * globalRatio;

			int adjustment = (int)Math.Round((1.0 - combinedScore) * 10);
			int newPriority = adjustment + (int)Math.Round(previousPriority * (combinedScore < 0.5 ? 1.0 : 0.3));

			//return Math.Clamp(newPriority, 1, 10);
			return newPriority;
		}

		public Dictionary<string,int> AllStudentPrioritiesUpdated()
		{
			// לעשות פונקציה של עידכון ברפוזיטורי
			foreach (Student student in students)
			{
				int actual = actualScores.ContainsKey(student.Id) ? actualScores[student.Id] : 0;
				int max = maxScores.ContainsKey(student.Id) ? maxScores[student.Id] : 1;
				int prev = student.Priority ?? 5;

				priorities[student.Id] = ComputeNewPriority(actual, max, totalScoreAllStudents, prev);
                _logger.LogInformation($"\n---- Priority of student :{student.Id} _ {priorities[student.Id]}");

            }
            return priorities;
		} 
		public async Task UpdateStudentAndChairs()
		{
			foreach (Student student in students)
			{
                _logger.LogInformation($"Priorities keys: {string.Join(",", priorities.Keys)}");
                _logger.LogInformation($"ChairOfStudent keys: {string.Join(",", chairOfStudent.Keys)}");
                _logger.LogInformation($"Students: {string.Join(",", students.Select(s => s.Id))}");

                //check
                _logger.LogInformation($"Updating student {student.Name}: old priority = {student.Priority}, new priority = {priorities[student.Id]}");
                student.Priority = priorities.ContainsKey(student.Id)? priorities[student.Id]:1;
				student.ChairId = chairOfStudent.ContainsKey(student.Id)? chairOfStudent[student.Id]:0;
				

				List<int> history = student.HistoryChairs ?? new List<int>();
				if (history.Count >= 3)
				{
					history.RemoveAt(0);
				}
				history.Add(student.ChairId.Value);
				student.HistoryChairs = history;
                //repository student

				

                await studentRepository.UpdateItem(student.Id, student);
				//check
                var updated = await studentRepository.GetById(student.Id);
                _logger.LogInformation($"Student {updated.Name} saved with priority = {updated.Priority}");


                Chair chair = await chairRepository.GetById(student.ChairId.Value);
				chair.StudentId = student.Id;
				await chairRepository.UpdateItem(chair.Id, chair);
            }
        }

        //public async Task CleanChairs(List<Chair> chairs)
        //{
        //    foreach (var chair in chairs)
        //    {
        //        chair.StudentId = null;
        //        await chairRepository.UpdateItem(chair.Id, chair);
        //        _logger.LogInformation($"Chair {chair.Id} cleaned. StudentId: {chair.StudentId}");

        //    }
        //}

        public async Task APICalculatePriority(StudentContext context, CpSolver solver)
		{
			this.context = context;
			this.solver = solver;

			students = context.Students;
			chairs = context.Chairs;
			chairOfStudent = context.InlayChairOfStudent;
			scoringRules = context.ScoringRules;
			priorities = new Dictionary<string, int>();
			foreach (Student s in students)
			{
				priorities[s.Id] = 0;
			}
            //זימון כל הפונקציות
            this.actualScores = await this.ComputeActualScores();
			this.maxScores = this.ComputeMaxScores();
			this.totalScoreAllStudents = this.ComputeTotalScoreAllStudents();
			this.priorities = this.AllStudentPrioritiesUpdated();
		}
	}
}
