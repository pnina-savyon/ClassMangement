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
		//security

		public virtual UserDto Authenticate(UserLogin value)
		{
			UserDto user= mapper.Map < User, UserDto> (repository.GetAll().FirstOrDefault(x => x.Password == value.Password && x.Email == value.Email));
			if (user != null)
				return user;
			return null;
		}

		public virtual string Generate(UserDto user)
		{
			var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
			var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
			var claims = new[] {
			new Claim(ClaimTypes.NameIdentifier,user.Name),
			new Claim(ClaimTypes.Email,user.Email),
			new Claim(ClaimTypes.PostalCode,user.Password),
			new Claim(ClaimTypes.Role,user.Role.ToString()),

			};
			var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public virtual UserDto GetCurrentUser()
		{
			var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
			if (identity != null)
			{
				var UserClaim = identity.Claims;
				return new StudentDto()
				{
					Name = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
					Email = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
					Password = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.PostalCode)?.Value,
					Role = Enum.TryParse(UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value, out Roles role) ? role : Roles.None

				};
			}
			return null;
		}

	}
}
