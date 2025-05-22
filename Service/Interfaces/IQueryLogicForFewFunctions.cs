using Common.Dto;
using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IQueryLogicForFewFunctions<T,Y>
    {
        Task<T> GetByIdLogic(Y id,Roles role, string userId);

        Task<T> DeleteLogic(Y id,Roles role, string userId);
    }
}
