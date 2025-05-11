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
    public class MarkService: IService<MarkDto, int>
	{
        private readonly IRepository<Mark, int> repository;
        private readonly IMapper mapper;
        public MarkService(IRepository<Mark, int> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }
        public MarkDto AddItem(MarkDto item)
        {
            return mapper.Map<Mark, MarkDto>(repository.AddItem(mapper.Map<MarkDto, Mark>(item)));
        }

        public MarkDto DeleteItem(int id)
        {
            return mapper.Map<Mark, MarkDto>(repository.DeleteItem(id));
        }

        public List<MarkDto> GetAll()
        {
            return mapper.Map<List<Mark>, List<MarkDto>>(repository.GetAll());
        }

        public MarkDto GetById(int id)
        {
            return mapper.Map<Mark, MarkDto>(repository.GetById(id));
        }

        public MarkDto UpdateItem(int id, MarkDto item)
        {
            return mapper.Map<Mark, MarkDto>(repository.UpdateItem(id, mapper.Map<MarkDto, Mark>(item)));
        }
    }
}
