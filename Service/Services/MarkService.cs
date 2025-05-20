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
        public async Task<MarkDto> AddItem(MarkDto item)
        {
            return mapper.Map<Mark, MarkDto>(await repository.AddItem(mapper.Map<MarkDto, Mark>(item)));
        }

        public async Task<MarkDto> DeleteItem(int id)
        {
            return mapper.Map<Mark, MarkDto>(await repository.DeleteItem(id));
        }

        public async Task<List<MarkDto>> GetAll()
        {
            return mapper.Map<List<Mark>, List<MarkDto>>(await repository.GetAll());
        }

        public async Task<MarkDto> GetById(int id)
        {
            return mapper.Map<Mark, MarkDto>(await repository.GetById(id));
        }

        public async Task<MarkDto> UpdateItem(int id, MarkDto item)
        {
            return mapper.Map<Mark, MarkDto>(await repository.UpdateItem(id, mapper.Map<MarkDto, Mark>(item)));
        }
    }
}
