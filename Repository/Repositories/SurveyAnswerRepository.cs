using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
	public class SurveyAnswerRepository: IRepository<SurveyAnswer, int>
	{
		private readonly IContext context;

		public SurveyAnswerRepository(IContext context)
		{
			this.context = context;
		}
		public SurveyAnswer AddItem(SurveyAnswer item)
		{
			this.context.SurveyAnswers.Add(item);
			this.context.Save();
			return item;
		}

		public SurveyAnswer DeleteItem(int id)
		{
			SurveyAnswer item = GetById(id);
			this.context.SurveyAnswers.Remove(item);
			this.context.Save();
			return item;
		}

		public List<SurveyAnswer> GetAll()
		{
			return this.context.SurveyAnswers.ToList();
		}

		public SurveyAnswer GetById(int id)
		{
			return this.context.SurveyAnswers.FirstOrDefault(x => x.Id == id);
		}

		public SurveyAnswer UpdateItem(int id, SurveyAnswer item)
		{
			SurveyAnswer surveyAnswer = GetById(id);
			surveyAnswer.CountOfVotes = item.CountOfVotes;
			surveyAnswer.Content = item.Content;
			surveyAnswer.SupportingStudents = item.SupportingStudents;
			this.context.Save();
			return surveyAnswer;
		}
	}
}
