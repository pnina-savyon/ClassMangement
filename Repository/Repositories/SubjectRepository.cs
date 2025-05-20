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
	public class SubjectRepository: IRepository<Subject, int>
	{
		private readonly IContext context;

		public SubjectRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<Subject> AddItem(Subject item)
		{
			await this.context.Subjects.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<Subject> DeleteItem(int id)
		{
			Subject item = await GetById(id);
			this.context.Subjects.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<Subject>> GetAll()
		{
			return await this.context.Subjects.ToListAsync();
		}

		public async Task<Subject> GetById(int id)
		{
			return await this.context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<Subject> UpdateItem(int id, Subject item)
		{
			Subject subject = await GetById(id);
			subject.Name = item.Name;
			await this.context.Save();
			return subject;
		}
	}
}
