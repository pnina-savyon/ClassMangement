using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class DailyAttendance
	{
		[Key]
		public int Id { get; set; }
		public string StudentId { get; set; }
		[ForeignKey("StudentId")]
		public virtual Student Student { get; set; }

		public DateTime DateOfDay { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
		public Statuses Status { get; set; }
		public string Notes { get; set; }
	}
}
