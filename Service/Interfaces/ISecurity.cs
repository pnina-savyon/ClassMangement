using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
	public interface ISecurity<T,Y>
	{
		string Generate(T user);
		T Authenticate(Y value);
		T GetCurrentUser();
        string Login(Y value);
    }
}
