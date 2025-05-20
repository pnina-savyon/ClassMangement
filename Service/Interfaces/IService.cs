using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
	public interface IService<T,TKey>
	{
		Task<T> GetById(TKey id);
		Task<List<T>> GetAll();

		Task<T> AddItem(T item);

		Task<T> DeleteItem(TKey id);
		Task<T> UpdateItem(TKey id, T item);
	}
}
