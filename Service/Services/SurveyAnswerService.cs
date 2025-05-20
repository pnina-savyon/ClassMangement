using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class SurveyAnswerService: IService<SurveyAnswerDto, int>
	{
		//service מכיר גם את common וגם , ריפוזיטורי?
		//לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
		private readonly IRepository<SurveyAnswer, int> repository;
		private readonly IMapper mapper;
		public SurveyAnswerService(IRepository<SurveyAnswer, int> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;

		}
		public async Task<SurveyAnswerDto> AddItem(SurveyAnswerDto item)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(await repository.AddItem(mapper.Map<SurveyAnswerDto, SurveyAnswer>(item)));
		}

		//public async Task<SurveyAnswer> AddItem(SurveyAnswer item)
		//{
		//	throw new NotImplementedException();
		//}

		public async Task<SurveyAnswerDto> DeleteItem(int id)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(await repository.DeleteItem(id));
		}

		public async Task<List<SurveyAnswerDto>> GetAll()
		{
			return mapper.Map<List<SurveyAnswer>, List<SurveyAnswerDto>>(await repository.GetAll());
		}

		public async Task<SurveyAnswerDto> GetById(int id)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(await repository.GetById(id));
		}


		public async Task<SurveyAnswerDto> UpdateItem(int id, SurveyAnswerDto item)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(await repository.UpdateItem(id, mapper.Map<SurveyAnswerDto, SurveyAnswer>(item)));
		}
	}
}
