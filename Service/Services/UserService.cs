using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Entities.Enums;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public abstract class UserService<TUser, TUserDto> : IService<TUserDto, string> where TUser : User where TUserDto : UserDto
	{
		protected readonly IRepository<TUser, string> repository;
		protected readonly IHttpContextAccessor _httpContextAccessor;
		protected readonly IMapper mapper;
		protected readonly ISecurity<UserDto, UserLogin> _security;
		protected readonly IConfiguration config;


		public UserService(IRepository<TUser, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, ISecurity<UserDto, UserLogin> security, IConfiguration config)
		{
			this.repository = repository;
			_httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			_security = security;
			this.config = config;
		}
		
		public abstract TUserDto AddItem(TUserDto item);

		public abstract TUserDto DeleteItem(string id);

		public abstract List<TUserDto> GetAll();

		public abstract TUserDto GetById(string id);

		public abstract TUserDto UpdateItem(string id, TUserDto item);
		

	}
}
