using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class SurveyDto
	{
		public int? Id { get; set; }
		// אין קשר בין סקר לכיתה ?
		public int ClassId { get; set; }
		public string? QuestionContent { get; set; }
	}
}