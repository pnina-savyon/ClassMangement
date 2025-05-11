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
	public class SubjectService: IService<SubjectDto, int>
	{
		//service מכיר גם את common וגם , ריפוזיטורי?
		//לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
		private readonly IRepository<Subject, int> repository;
		private readonly IMapper mapper;
		public SubjectService(IRepository<Subject, int> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;

		}
		public SubjectDto AddItem(SubjectDto item)
		{
			return mapper.Map<Subject, SubjectDto>(repository.AddItem(mapper.Map<SubjectDto, Subject>(item)));
		}

		public SubjectDto DeleteItem(int id)
		{
			return mapper.Map<Subject, SubjectDto>(repository.DeleteItem(id));
		}

		public List<SubjectDto> GetAll()
		{
			return mapper.Map<List<Subject>, List<SubjectDto>>(repository.GetAll());
		}

		public SubjectDto GetById(int id)
		{
			return mapper.Map<Subject, SubjectDto>(repository.GetById(id));
		}

		public SubjectDto UpdateItem(int id, SubjectDto item)
		{
			return mapper.Map<Subject, SubjectDto>(repository.UpdateItem(id, mapper.Map<SubjectDto, Subject>(item)));
		}
	}
}
