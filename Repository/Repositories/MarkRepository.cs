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
		public Mark AddItem(Mark item)
		{
			this.context.Marks.Add(item);
			this.context.Save();
			return item;
		}

		public Mark DeleteItem(int id)
		{
			Mark item = GetById(id);
			this.context.Marks.Remove(item);
			this.context.Save();
			return item;
		}

		public List<Mark> GetAll()
		{
			return this.context.Marks.ToList();
		}

		public Mark GetById(int id)
		{
			return this.context.Marks.FirstOrDefault(x => x.Id == id);
		}

		public Mark UpdateItem(int id, Mark item)
		{
			Mark mark = GetById(id);
			mark.MarkPercent = item.MarkPercent;
			mark.DateOfTest = item.DateOfTest;
			this.context.Save();
			return mark;
		}
	}
}
