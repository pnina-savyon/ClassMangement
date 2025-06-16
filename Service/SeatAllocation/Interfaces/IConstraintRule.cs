using Google.OrTools.ConstraintSolver;
using Google.OrTools.LinearSolver;
using Repository.Entities;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Interfaces
{
	public interface IConstraintRule
	{
		LinearExpr GetScore(Student student, IntVar studentChairVar, StudentContext context);
	}
}
