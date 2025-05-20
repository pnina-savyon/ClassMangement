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
	public class ChairService: IService<ChairDto, int>
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

		public async Task<List<ChairDto>> GetAll()
		{
			return mapper.Map<List<Chair>, List<ChairDto>>(await repository.GetAll());
		}

		public async Task<ChairDto> GetById(int id)
		{
			return mapper.Map<Chair, ChairDto>(await repository.GetById(id));
		}

		public async Task<ChairDto> UpdateItem(int id, ChairDto item)
		{
			return mapper.Map<Chair, ChairDto>(await repository.UpdateItem(id, mapper.Map<ChairDto, Chair>(item)));
		}
	}
}
