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
	public class SecurityService : ISecurity<UserDto,UserLogin>
	{
		
		protected readonly IRepository<User, string> repository;
		protected readonly IMapper mapper;
		protected readonly IConfiguration config;
		protected readonly IHttpContextAccessor _httpContextAccessor;


		public SecurityService(IRepository<User, string> repository, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.config = config;
			_httpContextAccessor = httpContextAccessor;
		}

		public virtual UserDto Authenticate(UserLogin value)
		{
			UserDto user = mapper.Map<User, UserDto>(repository.GetAll().FirstOrDefault(x => x.Password == value.Password && x.Email == value.Email));
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
