using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class SurveyAnswer
	{
		
		[Key]
		public int Id { get; set; }

		public int SurveyId { get; set; }
		[ForeignKey("SurveyId")]
		public virtual Survey Survey { get; set; }

		public int CountOfVotes { get; set; }

		public string Content { get; set; }

		public virtual ICollection<Student>? SupportingStudents { get; set; }
	}
}
