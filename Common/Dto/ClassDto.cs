﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class ClassDto
	{
		public int? Id { get; set; }
        public string? TeacherId { get; set; }
        public string? Password { get; set; }
		public string? Name { get; set; }
		public int? CountOfStudents { get; set; }
	}
}
