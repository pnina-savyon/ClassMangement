using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class MarkDto
	{
		public int SubjectId { get; set; }
		public SubjectDto? Subject { get; set; }
		public string StudentId { get; set; }
		public StudentDto? Student { get; set; }

		public int MarkPercent { get; set; }

		public DateTime DateOfTest { get; set; }
	}
}
