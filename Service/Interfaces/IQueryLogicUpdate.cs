using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
	public interface IQueryLogicUpdate<T,Y>
	{
		Task<T> UpdateLogic(Y id, string userId, T value);
	}
}
