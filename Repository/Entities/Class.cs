using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Class
	{
		[Key]
		public int Id { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }

		public string TeacherId { get; set; }
		[ForeignKey("TeacherId")]
		public virtual Teacher Teacher { get; set; }

		public int CountOfStudents { get; set; }
		public virtual ICollection<Chair>? Chairs { get; set; }
		public virtual ICollection<Student>? Students { get; set; }
		public virtual ICollection<Survey>? Surveys { get; set; }
	}
}
