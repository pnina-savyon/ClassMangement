﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Interfaces
{
	public interface IRuleSet
	{
		IEnumerable<IConstraintRule> GetConstraintRules();
		IEnumerable<IScoringRule> GetScoringRules();


	}
}
