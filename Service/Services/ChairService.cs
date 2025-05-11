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
		public ChairDto AddItem(ChairDto item)
		{
			return mapper.Map<Chair, ChairDto>(repository.AddItem(mapper.Map<ChairDto, Chair>(item)));
		}

		public ChairDto DeleteItem(int id)
		{
			return mapper.Map<Chair, ChairDto>(repository.DeleteItem(id));
		}

		public List<ChairDto> GetAll()
		{
			return mapper.Map<List<Chair>, List<ChairDto>>(repository.GetAll());
		}

		public ChairDto GetById(int id)
		{
			return mapper.Map<Chair, ChairDto>(repository.GetById(id));
		}

		public ChairDto UpdateItem(int id, ChairDto item)
		{
			return mapper.Map<Chair, ChairDto>(repository.UpdateItem(id, mapper.Map<ChairDto, Chair>(item)));
		}
	}
}
