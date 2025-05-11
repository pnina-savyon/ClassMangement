using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
	public class TeacherService: UserService<Teacher,TeacherDto>
	{

		public TeacherService(IRepository<Teacher, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, ISecurity<UserDto, UserLogin> security, IConfiguration config)
			:base(repository,httpContextAccessor,mapper,security,config)
		{

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
