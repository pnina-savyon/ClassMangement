using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Chair
	{
		[Key]
		public int Id { get; set; }
		public int ClassId { get; set; }

		[ForeignKey("ClassId")]
		public virtual Class Class { get; set; }
		public string? StudentId { get; set; }

		[ForeignKey("StudentId")]
		public virtual Student? CurrentStudent { get; set; }

		public int Row { get; set; }

		public int Column { get; set; }

		public bool IsNearTheDoor { get; set; }

		public bool IsNearTheWindow { get; set; }
	}
}
