using Common.Dto;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public abstract class UserService<TUserDto> : IService<TUserDto, string> where TUserDto : UserDto
	{
		public abstract TUserDto AddItem(TUserDto item);

		public abstract TUserDto DeleteItem(string id);

		public abstract List<TUserDto> GetAll();

		public abstract TUserDto GetById(string id);

		public abstract TUserDto UpdateItem(string id, TUserDto item);
	}
}
