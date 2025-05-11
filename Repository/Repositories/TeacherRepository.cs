using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class TeacherRepository: UserRepository<Teacher>
	{
		private readonly IContext context;

		public TeacherRepository(IContext context)
		{
			this.context = context;
		}
		public override Teacher AddItem(Teacher item)
		{
			this.context.Teachers.Add(item);
			this.context.Save();
			return item;
		}

		
		public override Teacher DeleteItem(string id)
		{
			Teacher item = GetById(id);
			this.context.Teachers.Remove(item);
			this.context.Save();
			return item;
		}

		
		public override List<Teacher> GetAll()
		{
			return this.context.Teachers.ToList();
		}

		
		public override Teacher GetById(string id)
		{
			return this.context.Teachers.FirstOrDefault(t => t.Id.Equals(id));
		}

		

		public override Teacher UpdateItem(string id, Teacher item)
		{
			Teacher teacher = GetById(id);
			teacher.Password = item.Password;
			teacher.Name = item.Name;
			teacher.Address = item.Address;
			teacher.DateOfBirth = item.DateOfBirth;
			teacher.Classes = item.Classes;
			teacher.Role = item.Role;
			teacher.Phone = item.Phone;

			this.context.Save();
			return teacher;
		}

		
	}
}
