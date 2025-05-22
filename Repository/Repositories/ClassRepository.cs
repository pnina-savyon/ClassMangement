using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class ClassRepository: IRepository<Class, int>
	{
		private readonly IContext context;

		public ClassRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<Class> AddItem(Class item)
		{
			await this.context.Classes.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<Class> DeleteItem(int id)
		{
			Class item = await GetById(id);
			this.context.Classes.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<Class>> GetAll()
		{
			return await this.context.Classes.ToListAsync();
		}

		public async Task<Class> GetById(int id)
		{
			return await this.context.Classes
				.Include(c => c.Teacher)
				.Include(c => c.Students)
				.FirstOrDefaultAsync(c => c.Id.Equals(id));
		}

		public async Task<Class> UpdateItem(int id, Class item)
		{
			Class classItem = await GetById(id);
			classItem.Password = item.Password;
			classItem.Name = item.Name;
			classItem.CountOfStudents = item.CountOfStudents;
			classItem.Students = item.Students;
			classItem.Surveys = item.Surveys;
			classItem.TeacherId = item.TeacherId;
			classItem.Chairs = item.Chairs;
			await this.context.Save();
			return classItem;
		}
	}
}
