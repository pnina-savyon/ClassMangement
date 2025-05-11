using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Mark
	{
		[Key]
		public int Id { get; set; }

		public int SubjectId { get; set; }
		[ForeignKey("SubjectId")]
		public virtual Subject Subject { get; set; }

		public string StudentId { get; set; }
		[ForeignKey("StudentId")]
		public virtual Student Student { get; set; }

		public int MarkPercent { get; set; }

		public DateTime DateOfTest { get; set; }
	}
}
