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

		public TeacherService(IRepository<Teacher, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration config)
			:base(repository,httpContextAccessor,mapper,config)
		{

		}
		public override async Task<TeacherDto> AddItem(TeacherDto item)
		{
			return mapper.Map<Teacher, TeacherDto>(await repository.AddItem(mapper.Map<TeacherDto, Teacher>(item)));
		}

		public override async Task<TeacherDto> DeleteItem(string id)
		{
			return mapper.Map<Teacher, TeacherDto>(await repository.DeleteItem(id));
		}

		public override async Task<List<TeacherDto>> GetAll()
		{
			return mapper.Map<List<Teacher>, List<TeacherDto>>(await repository.GetAll());
		}

		public override async Task<TeacherDto> GetById(string id)
		{
			return mapper.Map<Teacher, TeacherDto>(await repository.GetById(id));
		}

		public override async Task<TeacherDto> UpdateItem(string id, TeacherDto item)
		{
			return mapper.Map<Teacher, TeacherDto>(await repository.UpdateItem(id, mapper.Map<TeacherDto, Teacher>(item)));
		}
	}
}
