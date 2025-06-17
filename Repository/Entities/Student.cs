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
	public class Student:User
	{

		public int ClassId { get; set; }
		[ForeignKey("ClassId")]
		public virtual Class Class { get; set; }
		public int? ChairId { get; set; }
		[ForeignKey("ChairId")]
		public virtual Chair? CurrentChair { get; set; }

		public Levels MoralLevel { get; set; }
		public Levels StatusSocial { get; set; }
		public Levels AttentionLevel { get; set; } 
		public string? ImageUrl { get; set; }
		public int? Priority { get; set; }

		//
        public string? HistoryChairsJson { get; set; }

        [NotMapped]
        public List<int> HistoryChairs
        {
            get => string.IsNullOrEmpty(HistoryChairsJson)
                ? new List<int>()
                : System.Text.Json.JsonSerializer.Deserialize<List<int>>(HistoryChairsJson);
            set => HistoryChairsJson = System.Text.Json.JsonSerializer.Serialize(value);
        }
        public virtual ICollection<Student>? FavoriteFriends { get; set; }
		public virtual ICollection<Student>? NonFavoriteFriends { get; set; }
		public virtual ICollection<Mark>? Marks { get; set; }
		public virtual ICollection<DailyAttendance>? DailyAttendances { get; set; }
        public override Roles Role { get; set; } = Roles.User;

    }
}
