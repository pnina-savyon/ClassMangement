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
	public class SurveyService: IService<SurveyDto, int>
	{
		//service מכיר גם את common וגם , ריפוזיטורי?
		//לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
		private readonly IRepository<Survey, int> repository;
		private readonly IMapper mapper;
		public SurveyService(IRepository<Survey, int> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;

		}
		public SurveyDto AddItem(SurveyDto item)
		{
			return mapper.Map<Survey, SurveyDto>(repository.AddItem(mapper.Map<SurveyDto, Survey>(item)));
		}

		public SurveyDto DeleteItem(int id)
		{
			return mapper.Map<Survey, SurveyDto>(repository.DeleteItem(id));
		}

		public List<SurveyDto> GetAll()
		{
			return mapper.Map<List<Survey>, List<SurveyDto>>(repository.GetAll());
		}

		public SurveyDto GetById(int id)
		{
			return mapper.Map<Survey, SurveyDto>(repository.GetById(id));
		}

		public SurveyDto UpdateItem(int id, SurveyDto item)
		{
			return mapper.Map<Survey, SurveyDto>(repository.UpdateItem(id, mapper.Map<SurveyDto, Survey>(item)));
		}
	}
}
