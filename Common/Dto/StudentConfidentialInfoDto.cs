using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class StudentConfidentialInfoDto
	{
		//set...?
		//אולי דרך הרשאות?
		public string StudentId { get; set; }
		public Levels StatusSocial { get; set; }
		public Levels AttentionLevel { get; set; }
		public Roles Role { get; set; }
	}
}
