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
	public class ChairRepository: IRepository<Chair, int>
	{

		private readonly IContext context;

		public ChairRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<Chair> AddItem(Chair item)
		{
			await this.context.Chairs.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<Chair> DeleteItem(int id)
		{
			Chair item = await GetById(id);
			this.context.Chairs.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<Chair>> GetAll()
		{
			return await this.context.Chairs
                  .Include(c => c.Class)
				  .ToListAsync();
		}

		public async Task<Chair> GetById(int id)
		{
			return await this.context.Chairs
				.Include(ch => ch.Class)
				.ThenInclude(c=>c.Students)
				.Include(ch => ch.CurrentStudent)
				.FirstOrDefaultAsync(ch => ch.Id == id);
		}

		public async Task<Chair> UpdateItem(int id, Chair item)
		{
			Chair chair = await GetById(id);
			chair.StudentId = item.StudentId != null ? item.StudentId : chair.StudentId;
			chair.CurrentStudent = item.CurrentStudent != null ? item.CurrentStudent : chair.CurrentStudent;
			chair.Row = item.Row != 0 ? item.Row : chair.Row;
			chair.Column = item.Column != 0 ? item.Column : chair.Column;
			chair.IsNearTheDoor = item.IsNearTheDoor;
			chair.IsNearTheWindow = item.IsNearTheWindow;
			await this.context.Save();
			return chair;
		}
	}
}
