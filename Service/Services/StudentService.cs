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
	public class StudentService : UserService<StudentDto>
	{
		//service מכיר גם את common וגם , ריפוזיטורי?
		//לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
		private readonly IRepository<Student, string> repository;
		private readonly IMapper mapper;
		public StudentService(IRepository<Student, string> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
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
