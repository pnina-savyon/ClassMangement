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
		public Chair AddItem(Chair item)
		{
			this.context.Chairs.Add(item);
			this.context.Save();
			return item;
		}

		public Chair DeleteItem(int id)
		{
			Chair item = GetById(id);
			this.context.Chairs.Remove(item);
			this.context.Save();
			return item;
		}

		public List<Chair> GetAll()
		{
			return this.context.Chairs.ToList();
		}

		public Chair GetById(int id)
		{
			return this.context.Chairs.FirstOrDefault(x => x.Id == id);
		}

		public Chair UpdateItem(int id, Chair item)
		{
			Chair chair = GetById(id);
			chair.StudentId = item.StudentId;
			chair.CurrentStudent = item.CurrentStudent;
			chair.Row = item.Row;
			chair.Column = item.Column;
			chair.IsNearTheDoor = item.IsNearTheDoor;
			chair.IsNearTheWindow = item.IsNearTheWindow;
			this.context.Save();
			return chair;
		}
	}
}
