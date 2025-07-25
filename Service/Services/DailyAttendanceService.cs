﻿using AutoMapper;
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
    public class DailyAttendanceService: IService<DailyAttendanceDto, int>
	{
        private readonly IRepository<DailyAttendance, int> repository;
        private readonly IMapper mapper;
        public DailyAttendanceService(IRepository<DailyAttendance, int> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }
        public async Task<DailyAttendanceDto> AddItem(DailyAttendanceDto item)
        {
            return mapper.Map<DailyAttendance, DailyAttendanceDto>(await repository.AddItem(mapper.Map<DailyAttendanceDto, DailyAttendance>(item)));
        }

        public async Task<DailyAttendanceDto> DeleteItem(int id)
        {
            return mapper.Map<DailyAttendance, DailyAttendanceDto>(await repository.DeleteItem(id));
        }

        public async Task<List<DailyAttendanceDto>>  GetAll()
        {
            return mapper.Map<List<DailyAttendance>, List<DailyAttendanceDto>>(await repository.GetAll());
        }

        public async Task<DailyAttendanceDto> GetById(int id)
        {
            return mapper.Map<DailyAttendance, DailyAttendanceDto>(await repository.GetById(id));
        }

        public async Task<DailyAttendanceDto> UpdateItem(int id, DailyAttendanceDto item)
        {
            return mapper.Map<DailyAttendance, DailyAttendanceDto>(await repository.UpdateItem(id, mapper.Map<DailyAttendanceDto, DailyAttendance>(item)));
        }
    }
}
