﻿using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public abstract class UserDto
	{
		public string Id { get; set; }

		public string Password { get; set; }

		public string Name { get; set; }

		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; }

		public string Email { get; set; }
		public string Phone { get; set; }
		public Roles Role { get; set; }

	}
}
