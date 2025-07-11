using Microsoft.AspNetCore.Http;
using Repository.Entities;
using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
	public class StudentDto:UserDto
	{	
		public int? ClassId { get; set; }
		//public ClassDto classItem { get; set; }
		public int? ChairId { get; set; }
		
		//public Levels StatusSocial { get; set; }
		//public Levels AttentionLevel { get; set; }
		public byte[]? ArrImage { get; set; }
		public IFormFile? fileImage { get; set; }

        public ICollection<StudentDto>? FavoriteFriends { get; set; }
        public ICollection<StudentDto>? NonFavoriteFriends { get; set; }

    }
}
