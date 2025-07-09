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
using Service.SeatAllocation.Interfaces;

namespace Service.Services
{
	public class ClassService : IService<ClassDto, int>, IQueryLogicGeneric<ClassDto, int>,
		IServiceClass
	{
		private readonly IRepository<Class, int> repository;
		IRepositoryAllById<Chair, int> repositoryAllById;
        private readonly IMapper mapper;
		private readonly ISolver solver;
		public ClassService(IRepository<Class, int> repository, IMapper mapper, ISolver solver, IRepositoryAllById<Chair, int> repositoryAllById)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.solver = solver;
			this.repositoryAllById = repositoryAllById;
		}
		public async Task<ClassDto> AddItem(ClassDto item)
		{
			return mapper.Map<Class, ClassDto>(await repository.AddItem(mapper.Map<ClassDto, Class>(item)));
		}

		//public async Task<List<ChairDto>> AllChairsByClass(int id, Roles role, string userId)
		//{
		//	Class c = await repository.GetById(id);
		//	if (c == null || c.TeacherId == null || c.Chairs == null)
		//		return null;
		//	string teacherId = c.TeacherId;
		//	if (role == Roles.Admin)
		//		return teacherId == userId ? mapper.Map<List<Chair>,List<ChairDto>>(c.Chairs.ToList()) : null;
			
		//	return c.Students.Any(s => s.Id == userId) ? mapper.Map<List<Chair>, List<ChairDto>>(c.Chairs.ToList()) : null;
  //      }

        public async Task<List<ChairDto>> AllChairsByClass(int classId, Roles role, string userId)
        {
            List<Chair> chairs = await repositoryAllById.GetAllItemOfId(classId);

            if (!chairs.Any())
                return null;

            Class cls = chairs.First().Class;
            if (cls == null)
                return null;

            bool hasAccess = role switch
            {
                Roles.Master => true,
                Roles.Admin => cls.TeacherId == userId,
                Roles.User => cls.Students?.Any(s => s.Id == userId) == true,
                Roles.AuthorizedUser => cls.Students?.Any(s => s.Id == userId) == true,
                _ => false // כל רול אחר שלא ידוע – אין גישה
            };

            return hasAccess ? mapper.Map<List<ChairDto>>(chairs) : null;
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
			if (c == null || c.TeacherId == null)
				return null;
			string teacherId = c.TeacherId;
			if (role == Roles.Admin)
				return teacherId == userId ? await GetById(id) : null;
		
			return c.Students.Any(s => s.Id == userId) ? await GetById(id) : null;
		}

        public async Task<ClassDto> SeatingAllocationLogic(int id, string userId)
        {
            Class c = await repository.GetById(id);
            string teacherId = c.TeacherId;

			return teacherId == userId ? mapper.Map < Class, ClassDto >( await solver.SolverFunc(id)) : null;
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
