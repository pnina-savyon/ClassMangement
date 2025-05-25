using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Entities.Enums;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class SurveyService: IService<SurveyDto, int>, IQueryLogicGeneric<SurveyDto, int>
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
		public async Task<SurveyDto> AddItem(SurveyDto item)
		{
			return mapper.Map<Survey, SurveyDto>(await repository.AddItem(mapper.Map<SurveyDto, Survey>(item)));
		}

		public async Task<SurveyDto> DeleteItem(int id)
		{
			return mapper.Map<Survey, SurveyDto>(await repository.DeleteItem(id));
		}

		public Task<SurveyDto> DeleteLogic(int id, Roles role, string userId)
		{
			throw new NotImplementedException();
		}

		public async Task<List<SurveyDto>> GetAll()
		{
			return mapper.Map<List<Survey>, List<SurveyDto>>(await repository.GetAll());
		}

		public async Task<SurveyDto> GetById(int id)
		{
			return mapper.Map<Survey, SurveyDto>(await repository.GetById(id));
		}

		public async Task<SurveyDto> GetByIdLogic(int id, Roles role, string userId)
		{
			if (role == Roles.Master)
				return GetById(id);
			Survey s = await repository.GetById(id);
			if (s == null || s.Class == null || s.Class.TeacherId == null)
				return null;
			string teacherId = s.Class.TeacherId;
			return teacherId == userId ? await GetById(id) : null;


		}

		public async Task<SurveyDto> UpdateItem(int id, SurveyDto item)
		{
			return mapper.Map<Survey, SurveyDto>(await repository.UpdateItem(id, mapper.Map<SurveyDto, Survey>(item)));
		}

		public Task<SurveyDto> UpdateLogic(int id, string userId, SurveyDto value)
		{
			throw new NotImplementedException();
		}
	}
}
