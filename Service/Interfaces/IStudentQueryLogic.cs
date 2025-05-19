using Common.Dto;
using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStudentQueryLogic
    {
        StudentDto GetByIdLogic(string id,Roles role, string userId);

        StudentDto DeleteLogic(string id,Roles role, string userId);

        StudentConfidentialInfoDto UpdateItemForTeacher(string id, StudentConfidentialInfoDto value);

        StudentConfidentialInfoDto UpdateLogicForTeacher(string id, string userId
            , StudentConfidentialInfoDto value);
    }
}
