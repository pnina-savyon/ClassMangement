﻿using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class ChairDto
	{
		public int? Id { get; set; }
		public int? ClassId { get; set; }	
		public string? StudentId { get; set; }
        public int SerialNumberByClass { get; set; }
        //public int? Row { get; set; }

		//public int? Column { get; set; }

		public bool? IsNearTheDoor { get; set; }

		public bool? IsNearTheWindow { get; set; }

        //public ICollection<ChairDto>? NearbyChairs { get; set; }

    }
}
