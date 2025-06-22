using Google.OrTools.LinearSolver;
using Google.OrTools.Sat;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.SeatAllocation.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Solver
{
	public class Solver : ISolver
	{
		private StudentContext studentContext { get; set; }
		private IRepository<Class,int> ClassRepository { get; set; }
        private int ClassId { get;  set; }


        public Solver(IRepository<Class, int> classRepository, int classId)
		{
			ClassRepository = classRepository;
			ClassId = classId;
		}
		public async Task BuildSolver()
		{
			Class c = await ClassRepository.GetById(ClassId);
			studentContext = new StudentContext(c.Students.ToList(), c.Chairs.ToList());

			IEnumerable<IConstraintRule> constraintRules = studentContext.GetConstraintRules();
			IEnumerable<IScoringRule> scoringRules = studentContext.GetScoringRules();
			foreach(IConstraintRule constraintRule in constraintRules)
			{
				constraintRule.Apply(studentContext.Model,studentContext);
			}
			foreach(Student student in studentContext.Students)
			{
				studentContext.StudentChairVars[student.Id] = studentContext.Model.NewIntVar(1,studentContext.NumChairs, $"chair_of_student_{student.Id}");
				foreach (IScoringRule scoringRule in scoringRules)
				{
					scoringRule.GetScore(student, studentContext.StudentChairVars[student.Id], studentContext);
				}
			}
		}
		public async Task SolverFunc()
		{
			await BuildSolver();		
			studentContext.Model.Maximize(studentContext.Objective);
			CpSolver solver = new CpSolver();
			CpSolverStatus status = solver.Solve(studentContext.Model);
			if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
			{
				foreach (Student student in studentContext.Students)
				{
					Console.WriteLine("The student: " + student.Name+"in chair: " + studentContext.StudentChairVars[student.Id]);
                }
				//מעבר על dictationary
			}
			else
			{
				Console.WriteLine("solution not found, status:"+status);
			}
		}
	}

	// model.Maximize(objective);

	// CpSolver solver = new CpSolver();
	// CpSolverStatus status = solver.Solve(model);

	// if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
	// {
	//     Console.WriteLine("שיבוץ ימי עבודה לחודש:");
	//     for (int d = 0; d < totalDays; d++)
	//     {
	//         Console.Write($"יום {d + 1}: ");
	//         for (int e = 0; e < numEmployees; e++)
	//         {
	//             if (solver.Value(isWorking[e, d]) == 1)
	//             {
	//                 Console.Write(employees[e] + " ");
	//             }
	//         }
	//         Console.WriteLine();
	//     }
	// }
	// else
	// {
	//     Console.WriteLine("לא נמצא פתרון.");
	// }
}
