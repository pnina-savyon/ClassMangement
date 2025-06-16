using Google.OrTools.Sat;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Rules
{
	internal class AllDifferentConstraintRule : IConstraintRule
	{
		public void Apply(CpModel model, StudentContext context)
		{
			IntVar[] chairs = context.StudentChairVars.Values.ToArray();
			model.AddAllDifferent(chairs);
		}
	}
}
