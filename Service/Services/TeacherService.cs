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
	public class TeacherService: UserService<TeacherDto>
	{
		private readonly IRepository<Teacher, string> repository;
		private readonly IMapper mapper;
		public TeacherService(IRepository<Teacher, string> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}
		public override TeacherDto AddItem(TeacherDto item)
		{
			return mapper.Map<Teacher, TeacherDto>(repository.AddItem(mapper.Map<TeacherDto, Teacher>(item)));
		}

		public override TeacherDto DeleteItem(string id)
		{
			return mapper.Map<Teacher, TeacherDto>(repository.DeleteItem(id));
		}

		public override List<TeacherDto> GetAll()
		{
			return mapper.Map<List<Teacher>, List<TeacherDto>>(repository.GetAll());
		}

		public override TeacherDto GetById(string id)
		{
			return mapper.Map<Teacher, TeacherDto>(repository.GetById(id));
		}

		public override TeacherDto UpdateItem(string id, TeacherDto item)
		{
			return mapper.Map<Teacher, TeacherDto>(repository.UpdateItem(id, mapper.Map<TeacherDto, Teacher>(item)));
		}
	}
}
