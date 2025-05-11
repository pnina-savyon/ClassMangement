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
		public DailyAttendance AddItem(DailyAttendance item)
		{
			this.context.DailyAttendances.Add(item);
			this.context.Save();
			return item;
		}

		public DailyAttendance DeleteItem(int id)
		{
			DailyAttendance item = GetById(id);
			this.context.DailyAttendances.Remove(item);
			this.context.Save();
			return item;
		}

		public List<DailyAttendance> GetAll()
		{
			return this.context.DailyAttendances.ToList();
		}

		public DailyAttendance GetById(int id)
		{
			return this.context.DailyAttendances.FirstOrDefault(x => x.Id == id);
		}

		public DailyAttendance UpdateItem(int id, DailyAttendance item)
		{
			DailyAttendance dailyAttendances = GetById(id);
			dailyAttendances.DateOfDay = item.DateOfDay;
			dailyAttendances.StartTime = item.StartTime;
			dailyAttendances.EndTime = item.EndTime;
			dailyAttendances.Status = item.Status;
			dailyAttendances.Notes = item.Notes;
			this.context.Save();
			return dailyAttendances;
		}
	}
}
