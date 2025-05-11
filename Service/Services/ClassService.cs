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
        public ClassDto AddItem(ClassDto item)
        {
            return mapper.Map<Class, ClassDto>(repository.AddItem(mapper.Map<ClassDto, Class>(item)));
        }

        public ClassDto DeleteItem(int id)
        {
            return mapper.Map<Class, ClassDto>(repository.DeleteItem(id));
        }

        public List<ClassDto> GetAll()
        {
            return mapper.Map<List<Class>, List<ClassDto>>(repository.GetAll());
        }

        public ClassDto GetById(int id)
        {
            return mapper.Map<Class, ClassDto>(repository.GetById(id));
        }

        public ClassDto UpdateItem(int id, ClassDto item)
        {
            return mapper.Map<Class, ClassDto>(repository.UpdateItem(id, mapper.Map<ClassDto, Class>(item)));
        }
    }
}
