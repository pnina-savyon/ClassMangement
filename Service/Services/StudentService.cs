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
	public class StudentService : UserService<Student, StudentDto>
	{
		//service מכיר גם את common וגם , ריפוזיטורי?
		//לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
		
		public StudentService(IRepository<Student, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration config)
			: base(repository, httpContextAccessor, mapper, config)
		{

		}
		public override StudentDto AddItem(StudentDto item)
		{
			return mapper.Map<Student, StudentDto>(repository.AddItem(mapper.Map<StudentDto, Student>(item)));
		}

		public override StudentDto DeleteItem(string id)
		{
			return mapper.Map<Student, StudentDto>(repository.DeleteItem(id));
		}

		public override List<StudentDto> GetAll()
		{
			return mapper.Map<List<Student>, List<StudentDto>>(repository.GetAll());
		}

		public override StudentDto GetById(string id)
		{
			return mapper.Map<Student, StudentDto>(repository.GetById(id));
		}

		public override StudentDto UpdateItem(string id, StudentDto item)
		{
			return mapper.Map<Student, StudentDto>(repository.UpdateItem(id, mapper.Map<StudentDto, Student>(item)));
		}
	}
}
