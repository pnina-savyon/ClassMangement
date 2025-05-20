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
	public class DailyAttendanceRepository: IRepository<DailyAttendance, int>
	{
		private readonly IContext context;

		public DailyAttendanceRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<DailyAttendance> AddItem(DailyAttendance item)
		{
			await this.context.DailyAttendances.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<DailyAttendance> DeleteItem(int id)
		{
			DailyAttendance item = await GetById(id);
			this.context.DailyAttendances.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<DailyAttendance>> GetAll()
		{
			return await this.context.DailyAttendances.ToListAsync();
		}

		public async Task<DailyAttendance> GetById(int id)
		{
			return await this.context.DailyAttendances.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<DailyAttendance> UpdateItem(int id, DailyAttendance item)
		{
			DailyAttendance dailyAttendances = await GetById(id);
			dailyAttendances.DateOfDay = item.DateOfDay;
			dailyAttendances.StartTime = item.StartTime;
			dailyAttendances.EndTime = item.EndTime;
			dailyAttendances.Status = item.Status;
			dailyAttendances.Notes = item.Notes;
			await this.context.Save();
			return dailyAttendances;
		}
	}
}
