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
	public class MarkRepository: IRepository<Mark, int>
	{
		private readonly IContext context;

		public MarkRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<Mark> AddItem(Mark item)
		{
			await this.context.Marks.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<Mark> DeleteItem(int id)
		{
			Mark item = await GetById(id);
			this.context.Marks.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<Mark>> GetAll()
		{
			return await this.context.Marks.ToListAsync();
		}

		public async Task<Mark> GetById(int id)
		{
			return await this.context.Marks.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Mark> UpdateItem(int id, Mark item)
		{
			Mark mark = await GetById(id);
			mark.MarkPercent = item.MarkPercent;
			mark.DateOfTest = item.DateOfTest;
			await this.context.Save();
			return mark;
		}
	}
}
