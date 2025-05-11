using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
	public interface IService<T,TKey>
	{
		T GetById(TKey id);
		List<T> GetAll();

		T AddItem(T item);

		T DeleteItem(TKey id);
		T UpdateItem(TKey id, T item);
	}
}
