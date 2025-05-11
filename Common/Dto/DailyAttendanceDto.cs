using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class DailyAttendanceDto
	{
		public int Id { get; set; }

		public DateTime DateOfDay { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }
		public Statuses Status { get; set; }
		public string Notes { get; set; }
	}
}
