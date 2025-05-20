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
		protected readonly IConfiguration config;


		public UserService(IRepository<TUser, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration config)
		{
			this.repository = repository;
			_httpContextAccessor = httpContextAccessor;
			this.mapper = mapper;
			this.config = config;
		}
		
		public abstract Task<TUserDto> AddItem(TUserDto item);

		public abstract Task<TUserDto> DeleteItem(string id);

		public abstract Task<List<TUserDto>> GetAll();

		public abstract Task<TUserDto> GetById(string id);

		public  abstract Task<TUserDto> UpdateItem(string id, TUserDto item);
		

	
	}
}
