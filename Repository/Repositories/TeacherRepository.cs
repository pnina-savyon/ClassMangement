using Repository.Entities;
using Repository.Entities.Enums;
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
			if(item !=null)
			{
                this.context.Teachers.Remove(item);
                this.context.Save();
            }		
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

            if (teacher == null)
				return null;

            teacher.Password = item.Password != null ? item.Password : teacher.Password;
			teacher.Name = item.Name != null ? item.Name : teacher.Name;
			teacher.Address = item.Address != null ? item.Address : teacher.Address;
			teacher.DateOfBirth = item.DateOfBirth != null ? item.DateOfBirth : teacher.DateOfBirth;
			teacher.Classes = item.Classes != null ? item.Classes : teacher.Classes;
			teacher.Role = item.Role != Roles.None ? item.Role : teacher.Role;
			teacher.Phone = item.Phone != null ? item.Phone : teacher.Phone;

			this.context.Save();
			return teacher;
		}

		
	}
}
