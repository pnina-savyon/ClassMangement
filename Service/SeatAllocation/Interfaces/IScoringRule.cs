using Google.OrTools.Sat;
using Repository.Entities;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Interfaces
{
	public interface IScoringRule
	{
		LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context);
		int CalculateActualScore(Student student, Chair assignedChair, StudentContext context, CpSolver solver);
	}
}

