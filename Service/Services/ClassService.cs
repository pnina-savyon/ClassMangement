using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Entities.Enums;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class ClassService : IService<ClassDto, int>, IQueryLogicGeneric<ClassDto, int>
	{
		private readonly IRepository<Class, int> repository;
		private readonly IMapper mapper;
		public ClassService(IRepository<Class, int> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;

		}
		public async Task<ClassDto> AddItem(ClassDto item)
		{
			return mapper.Map<Class, ClassDto>(await repository.AddItem(mapper.Map<ClassDto, Class>(item)));
		}

		public async Task<ClassDto> DeleteItem(int id)
		{
			return mapper.Map<Class, ClassDto>(await repository.DeleteItem(id));
		}

		//.....
		public async Task<ClassDto> DeleteLogic(int id, Roles role, string userId)
		{
			Class c = await repository.GetById(id);
			string teacherId = c.TeacherId;

			return teacherId == userId ? await DeleteItem(id) : null;
		}

		public async Task<List<ClassDto>> GetAll()
		{
			return mapper.Map<List<Class>, List<ClassDto>>(await repository.GetAll());
		}

		public async Task<ClassDto> GetById(int id)
		{
			return mapper.Map<Class, ClassDto>(await repository.GetById(id));
		}

		public async Task<ClassDto> GetByIdLogic(int id, Roles role, string userId)
		{
			Class c = await repository.GetById(id);
			string teacherId = c.TeacherId;
			if (role == Roles.Admin)
				return teacherId == userId ? await GetById(id) : null;

			return c.Students.Any(s => s.Id == userId) ? await GetById(id) : null;
		}

		public async Task<ClassDto> UpdateItem(int id, ClassDto item)
		{
			return mapper.Map<Class, ClassDto>(await repository.UpdateItem(id, mapper.Map<ClassDto, Class>(item)));
		}

		public async Task<ClassDto> UpdateLogic(int id, string userId, ClassDto value)
		{
			Class c = await repository.GetById(id);
			string teacherId = c.TeacherId;

			return teacherId == userId ? await UpdateItem(id, value) : null;
		}
	}
}
