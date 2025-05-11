using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class SurveyRepository: IRepository<Survey, int>
	{
		private readonly IContext context;

		public SurveyRepository(IContext context)
		{
			this.context = context;
		}
		public Survey AddItem(Survey item)
		{
			this.context.Surveys.Add(item);
			this.context.Save();
			return item;
		}

		public Survey DeleteItem(int id)
		{
			Survey item = GetById(id);
			this.context.Surveys.Remove(item);
			this.context.Save();
			return item;
		}

		public List<Survey> GetAll()
		{
			return this.context.Surveys.ToList();
		}

		public Survey GetById(int id)
		{
			return this.context.Surveys.FirstOrDefault(s => s.Id.Equals(id));
		}

		public Survey UpdateItem(int id, Survey item)
		{
			//האם לעדכן גם את ה-class כולו???
			Survey survey = GetById(id);
			survey.ClassId = item.ClassId;
			survey.QuestionContent = item.QuestionContent;
			survey.Answers = item.Answers;

			this.context.Save();
			return survey;
		}
	}
}
