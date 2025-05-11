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
		public SurveyAnswerDto AddItem(SurveyAnswerDto item)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(repository.AddItem(mapper.Map<SurveyAnswerDto, SurveyAnswer>(item)));
		}

		public SurveyAnswer AddItem(SurveyAnswer item)
		{
			throw new NotImplementedException();
		}

		public SurveyAnswerDto DeleteItem(int id)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(repository.DeleteItem(id));
		}

		public List<SurveyAnswerDto> GetAll()
		{
			return mapper.Map<List<SurveyAnswer>, List<SurveyAnswerDto>>(repository.GetAll());
		}

		public SurveyAnswerDto GetById(int id)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(repository.GetById(id));
		}


		public SurveyAnswerDto UpdateItem(int id, SurveyAnswerDto item)
		{
			return mapper.Map<SurveyAnswer, SurveyAnswerDto>(repository.UpdateItem(id, mapper.Map<SurveyAnswerDto, SurveyAnswer>(item)));
		}
	}
}
