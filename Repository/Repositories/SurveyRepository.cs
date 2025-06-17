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
	public class SurveyRepository: IRepository<Survey, int>
	{
		private readonly IContext context;

		public SurveyRepository(IContext context)
		{
			this.context = context;
		}
		public async Task<Survey> AddItem(Survey item)
		{
			await this.context.Surveys.AddAsync(item);
			await this.context.Save();
			return item;
		}

		public async Task<Survey> DeleteItem(int id)
		{
			Survey item = await GetById(id);
			this.context.Surveys.Remove(item);
			await this.context.Save();
			return item;
		}

		public async Task<List<Survey>> GetAll()
		{
			return await this.context.Surveys.ToListAsync();
		}

		public async Task<Survey> GetById(int id)
		{
			return await this.context.Surveys
				.Include(s => s.Class)
				.ThenInclude(c => c.TeacherId)
				.FirstOrDefaultAsync(c => c.Id.Equals(id));
		}

		public async Task<Survey> UpdateItem(int id, Survey item)
		{
			//האם לעדכן גם את ה-class כולו???
			Survey survey = await GetById(id);
			survey.ClassId = item.ClassId != 0 ? item.ClassId : survey.ClassId;
			survey.QuestionContent = item.QuestionContent != null ? item.QuestionContent : survey.QuestionContent;
			survey.Answers = item.Answers == null ? survey.Answers : item.Answers;

			await this.context.Save();
			return survey;
		}
	}
}
