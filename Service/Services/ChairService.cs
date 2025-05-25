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
	public class ChairService: IService<ChairDto, int>, IQueryLogicGeneric<ChairDto,int>
	{

		private readonly IRepository<Chair, int> repository;
		private readonly IMapper mapper;
		public ChairService(IRepository<Chair, int> repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}
		public async Task<ChairDto> AddItem(ChairDto item)
		{
			return mapper.Map<Chair, ChairDto>(await repository.AddItem(mapper.Map<ChairDto, Chair>(item)));
		}

		public async Task<ChairDto> DeleteItem(int id)
		{
			return mapper.Map<Chair, ChairDto>(await repository.DeleteItem(id));
		}

        public async Task<ChairDto> DeleteLogic(int id, Roles role, string userId)
        {
            if (role == Roles.Master)
                return await DeleteItem(id);

            Chair chair = await repository.GetById(id);

            if (chair == null || chair.Class == null)
                return null;
            
			return userId == chair.Class.TeacherId ? await DeleteItem(id) : null;
        }

        public async Task<List<ChairDto>> GetAll()
		{
			return mapper.Map<List<Chair>, List<ChairDto>>(await repository.GetAll());
		}

		public async Task<ChairDto> GetById(int id)
		{
			return mapper.Map<Chair, ChairDto>(await repository.GetById(id));
		}

        public async Task<ChairDto> GetByIdLogic(int id, Roles role, string userId)
        {
			if (role == Roles.Master)
				return await GetById(id);

            Chair chair = await repository.GetById(id);
            if (chair == null || chair.Class == null)
				return null;
            string teacherId = chair.Class.TeacherId;
            string studentId = chair.StudentId;

            if (role == Roles.Admin)
				return userId == teacherId ? await GetById(id) : null;

			return userId == studentId ? await GetById(id) : null;
        }

        public async Task<ChairDto> UpdateItem(int id, ChairDto item)
		{
			return mapper.Map<Chair, ChairDto>(await repository.UpdateItem(id, mapper.Map<ChairDto, Chair>(item)));
		}

        public async Task<ChairDto> UpdateLogic(int id, string userId, ChairDto value)
        {
            Chair chair = await repository.GetById(id);

            if (chair == null || chair.Class == null)
                return null;

            return userId == chair.Class.TeacherId ? await UpdateItem(id,value) : null;
        }
    }
}
