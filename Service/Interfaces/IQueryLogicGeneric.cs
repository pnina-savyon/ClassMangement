using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
	public interface IQueryLogicGeneric<T,Y>:IQueryLogicUpdate<T,Y>, IQueryLogicForFewFunctions<T,Y>
	{
	}
}
