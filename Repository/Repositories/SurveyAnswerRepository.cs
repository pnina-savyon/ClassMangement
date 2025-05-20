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
	public class SurveyAnswerRepository: IRepository<SurveyAnswer, int>
	{
		private readonly IContext context;

		public SurveyAnswerRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<SurveyAnswer> AddItem(SurveyAnswer item)
		{
			await this.context.SurveyAnswers.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<SurveyAnswer> DeleteItem(int id)
		{
			SurveyAnswer item = await GetById(id);
			this.context.SurveyAnswers.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<SurveyAnswer>> GetAll()
		{
			return await this.context.SurveyAnswers.ToListAsync();
		}

		public async Task<SurveyAnswer> GetById(int id)
		{
			return await this.context.SurveyAnswers.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<SurveyAnswer> UpdateItem(int id, SurveyAnswer item)
		{
			SurveyAnswer surveyAnswer = await GetById(id);
			surveyAnswer.CountOfVotes = item.CountOfVotes;
			surveyAnswer.Content = item.Content;
			surveyAnswer.SupportingStudents = item.SupportingStudents;
			await this.context.Save();
			return surveyAnswer;
		}
	}
}
