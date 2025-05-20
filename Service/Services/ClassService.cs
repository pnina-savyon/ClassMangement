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
    public class ClassService: IService<ClassDto, int>
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

        public async Task<List<ClassDto>> GetAll()
        {
            return mapper.Map<List<Class>, List<ClassDto>>(await repository.GetAll());
        }

        public async Task<ClassDto> GetById(int id)
        {
            return mapper.Map<Class, ClassDto>(await repository.GetById(id));
        }

        public async Task<ClassDto> UpdateItem(int id, ClassDto item)
        {
            return mapper.Map<Class, ClassDto>(await repository.UpdateItem(id, mapper.Map<ClassDto, Class>(item)));
        }
    }
}
