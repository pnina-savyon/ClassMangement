using Common.Dto;
using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceClass
    {
        Task<ClassDto> SeatingAllocationLogic(int classId, string userId);
		Task<List<ClassDto>> AllClassByTeacher(Roles role, string userId);
		//Task<List<ChairDto>> AllChairsByClass(int classId, Roles role, string userId);
	}

}

