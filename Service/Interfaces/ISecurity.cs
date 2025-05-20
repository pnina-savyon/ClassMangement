using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
	public interface ISecurity<T,Y>
	{
		//dto,login?
		string Generate(T user);
		Task<T> Authenticate(Y value);
		T GetCurrentUser();
        Task<string> Login(Y value);
    }
}
