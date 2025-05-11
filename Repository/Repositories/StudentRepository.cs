using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class StudentRepository: UserRepository<Student>
	{
		private readonly IContext context;

		public StudentRepository(IContext context)
		{
			this.context = context;
		}
		public override Student AddItem(Student item)
		{
			this.context.Students.Add(item);
			this.context.Save();
			return item;
		}

		public override Student DeleteItem(string id)
		{
			Student item = GetById(id);
			this.context.Students.Remove(item);
			this.context.Save();
			return item;
		}

		public override List<Student> GetAll()
		{
			return this.context.Students.ToList();
		}

		public override Student GetById(string id)
		{
			return this.context.Students.FirstOrDefault(s => s.Id.Equals(id));
		}

		public override Student UpdateItem(string id, Student item)
		{

			Student student = GetById(id);
			student.Password = item.Password;
			student.Name = item.Name;
			student.Marks = item.Marks;
			student.ChairId = item.ChairId;
			student.Phone = item.Phone;
			student.Address = item.Address;
			student.Priority = item.Priority;
			student.StatusSocial = item.StatusSocial;
			student.FavoriteFriends = item.FavoriteFriends;
			student.ClassId = item.ClassId;
			student.Role = item.Role;
			student.AttentionLevel = item.AttentionLevel;
			student.ImageUrl = item.ImageUrl;
			student.HistoryChairs = item.HistoryChairs;
			student.DailyAttendances = item.DailyAttendances;

			this.context.Save();
			return student;
		}
	}
}
