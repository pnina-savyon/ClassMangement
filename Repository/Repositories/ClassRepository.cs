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
		public Class AddItem(Class item)
		{
			this.context.Classes.Add(item);
			this.context.Save();
			return item;
		}

		public Class DeleteItem(int id)
		{
			Class item = GetById(id);
			this.context.Classes.Remove(item);
			this.context.Save();
			return item;
		}

		public List<Class> GetAll()
		{
			return this.context.Classes.ToList();
		}

		public Class GetById(int id)
		{
			return this.context.Classes.FirstOrDefault(c => c.Id.Equals(id));
		}

		public Class UpdateItem(int id, Class item)
		{
			Class classItem = GetById(id);
			classItem.Password = item.Password;
			classItem.Name = item.Name;
			classItem.CountOfStudents = item.CountOfStudents;
			classItem.Students = item.Students;
			classItem.Surveys = item.Surveys;
			classItem.TeacherId = item.TeacherId;
			classItem.Chairs = item.Chairs;
			this.context.Save();
			return classItem;
		}
	}
}
