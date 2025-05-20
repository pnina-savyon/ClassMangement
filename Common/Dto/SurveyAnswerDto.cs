using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class SurveyAnswerDto
	{
		public int? Id { get; set; }

		public int? CountOfVotes { get; set; }

		public string? Content { get; set; }
	}
}
