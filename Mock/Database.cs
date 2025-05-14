using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock
{
	public class Database: DbContext, IContext
	{
		public DbSet<Student> Students { get; set; }
		//public DbSet<Teacher> Teacher { get; set; }
		public DbSet<Chair> Chairs { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Mark> Marks { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<DailyAttendance> DailyAttendances { get; set; }
		public DbSet<Survey> Surveys { get; set; }
		public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
		public DbSet<Teacher> Teachers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// ירושה של Student ו-Teacher מ-User
			modelBuilder.Entity<User>()
				.HasDiscriminator<string>("UserType")
				.HasValue<Student>("Student")
				.HasValue<Teacher>("Teacher");

			// מניעת מחיקת שרשרת (Cascade) בין Student ל-Class
			modelBuilder.Entity<Student>()
				.HasOne(s => s.Class)
				.WithMany(c => c.Students) // אם Class כולל ICollection<Student>
				.HasForeignKey(s => s.ClassId)
				.OnDelete(DeleteBehavior.Restrict); // או DeleteBehavior.NoAction

			// מניעת מחיקת שרשרת עם Chair
			modelBuilder.Entity<Student>()
				.HasOne(s => s.CurrentChair)
				.WithMany()
				.HasForeignKey(s => s.ChairId)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public void Save()
		{
			SaveChanges();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-1VUANBN;Database=ClassManagementDB;trusted_connection=true;TrustServerCertificate=true");
		}



	}
}
