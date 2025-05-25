using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryAllById<T,Y>
    {
        Task<List<T>> GetAllItemOfId(Y id);
    }
}
