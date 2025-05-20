using Microsoft.EntityFrameworkCore;
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
		public override async Task<Teacher> AddItem(Teacher item)
		{
            await this.context.Teachers.AddAsync(item);
            await this.context.Save();
			return item;
		}	
		public override async Task<Teacher> DeleteItem(string id)
		{
			Teacher item = await GetById(id);
			if(item !=null)
			{
                this.context.Teachers.Remove(item);
                await this.context.Save();
            }		
			return item;
		}

		
		public override async Task<List<Teacher>> GetAll()
		{
            return await this.context.Teachers.ToListAsync();
		}

		
		public override async Task<Teacher> GetById(string id)
		{
			return await this.context.Teachers.FirstOrDefaultAsync(t => t.Id.Equals(id));
		}

		public override async Task<Teacher> UpdateItem(string id, Teacher item)
		{
			Teacher teacher = await GetById(id);

            if (teacher == null)
				return null;

            teacher.Password = item.Password != null ? item.Password : teacher.Password;
			teacher.Name = item.Name != null ? item.Name : teacher.Name;
			teacher.Address = item.Address != null ? item.Address : teacher.Address;
			teacher.DateOfBirth = item.DateOfBirth != null ? item.DateOfBirth : teacher.DateOfBirth;
			teacher.Classes = item.Classes != null ? item.Classes : teacher.Classes;
			teacher.Role = item.Role != Roles.None ? item.Role : teacher.Role;
			teacher.Phone = item.Phone != null ? item.Phone : teacher.Phone;

			await this.context.Save();
			return teacher;
		}

		
	}
}
