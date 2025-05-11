using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
	public interface IContext
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Chair> Chairs { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Mark> Marks { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<DailyAttendance> DailyAttendances { get; set; }
		public DbSet<Survey> Surveys { get; set; }
		public DbSet<SurveyAnswer> SurveyAnswers { get; set; }

		public void Save();
	}
}
