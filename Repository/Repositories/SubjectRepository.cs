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
		public Subject AddItem(Subject item)
		{
			this.context.Subjects.Add(item);
			this.context.Save();
			return item;
		}

		public Subject DeleteItem(int id)
		{
			Subject item = GetById(id);
			this.context.Subjects.Remove(item);
			this.context.Save();
			return item;
		}

		public List<Subject> GetAll()
		{
			return this.context.Subjects.ToList();
		}

		public Subject GetById(int id)
		{
			return this.context.Subjects.FirstOrDefault(s => s.Id == id);
		}

		public Subject UpdateItem(int id, Subject item)
		{
			Subject subject = GetById(id);
			subject.Name = item.Name;
			this.context.Save();
			return subject;
		}
	}
}
