using Google.OrTools.Sat;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.SeatAllocation.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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
		
        private readonly ILogger<Solver> _logger;
        private readonly ISeatingAllocationInputValidator seatingAllocationInputValidator;
		private IPostSolverAnalysis postSolverAnalysis { get; set; }



		public Solver(IRepository<Class, int> classRepository, ILogger<Solver> logger
			, ISeatingAllocationInputValidator seatingAllocationInputValidator,
				IPostSolverAnalysis postSolverAnalysis)
        {
            ClassRepository = classRepository;
            _logger = logger;
            this.seatingAllocationInputValidator = seatingAllocationInputValidator;
			this.postSolverAnalysis = postSolverAnalysis;	
        }

        public async Task BuildSolver()
		{
            Class c = await ClassRepository.GetById(ClassId);

            studentContext = new StudentContext(c.Students.ToList(), c.Chairs.ToList());
            IEnumerable<IConstraintRule> constraintRules = studentContext.GetConstraintRules();
			IEnumerable<IScoringRule> scoringRules = studentContext.GetScoringRules();
			studentContext.ScoringRules = scoringRules.ToList();//

			foreach (Student student in studentContext.Students)
            {
                if (!studentContext.StudentChairVars.ContainsKey(student.Id))
                {
                    studentContext.StudentChairVars[student.Id] = studentContext.Model.NewIntVar(1, studentContext.NumChairs, $"chair_of_student_{student.Id}");
                }
            }
			foreach (Student student in studentContext.Students)
			{
				foreach (IScoringRule scoringRule in scoringRules)
				{
                    LinearExpr scoreExpr = scoringRule.GetScore(student, studentContext.StudentChairVars[student.Id], studentContext);
                    studentContext.Objective.Add(scoreExpr);
                }
			}

			foreach (IConstraintRule constraintRule in constraintRules)
			{
				 constraintRule.Apply(studentContext.Model, studentContext);
			}
		}
		public async Task SolverFunc(int classId)
		{
            ClassId = classId;

            //validators chacking
            Class c = await ClassRepository.GetById(ClassId);
            if (!seatingAllocationInputValidator.IsValidInput(c.Students.ToList(), c.Chairs.ToList(), c))
                return;

            await BuildSolver();	
			
			studentContext.Model.Maximize(studentContext.Objective);
			CpSolver solver = new CpSolver();
			CpSolverStatus status = solver.Solve(studentContext.Model);
			if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
			{
				

				foreach (Student student in studentContext.Students)
				{
                    //Console.WriteLine("The student: " + student.Name+"in chair: " + studentContext.StudentChairVars[student.Id]);
                    //_logger.LogInformation("The student: {StudentName} in chair: {ChairVar}", student.Name, studentContext.StudentChairVars[student.Id].Domain);
                    _logger.LogInformation(
                        "The student: {StudentName} in chair: {ChairVar}, status: {Status}",
                        student.Name,
                        solver.Value(studentContext.StudentChairVars[student.Id]),
                        status);
					
						studentContext.InlayChairOfStudent[student.Id] = (int)solver.Value(studentContext.StudentChairVars[student.Id]);
                }
				postSolverAnalysis.APICalculatePriority(studentContext, solver);
				await postSolverAnalysis.UpdateStudentAndChairs();
				//מעבר על dictationary
			}
			else
			{
                _logger.LogInformation("solution not found, status:"+status);
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
