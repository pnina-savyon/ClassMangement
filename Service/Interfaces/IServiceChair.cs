using Common.Dto;
using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceChair
    {
        Task<List<ChairDto>> AllChairsOfClass(int classId, Roles role, string userId);
    }
}
