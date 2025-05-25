using Common.Dto;
using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceStudent
    {
        Task<List<StudentDto>> AllStudentsOfClass(int classId, Roles role, string userId);

    }
}
