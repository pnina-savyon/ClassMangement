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
    public class SecurityService<TUser, TUserDto> : ISecurity<TUserDto,UserLogin> 
		where TUser : User, new() where TUserDto : UserDto, new()
    {
		
		protected readonly IRepository<TUser, string> repository;
		protected readonly IMapper mapper;
		protected readonly IConfiguration config;
		protected readonly IHttpContextAccessor _httpContextAccessor;


		public SecurityService(IRepository<TUser, string> repository, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.config = config;
			_httpContextAccessor = httpContextAccessor;
		}

		public virtual TUserDto Authenticate(UserLogin value)
		{
			TUserDto user = mapper.Map<TUser, TUserDto>(repository.GetAll().FirstOrDefault(x => x.Password == value.Password && x.Email == value.Email));
			if (user != null)
				return user;
			return null;
		}

		public virtual string Generate(TUserDto user)
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

		public virtual TUserDto GetCurrentUser()
		{
			var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
			if (identity != null)
			{
				var UserClaim = identity.Claims;
				return new TUserDto()
				{
					Name = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
					Email = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
					Password = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.PostalCode)?.Value,
					Role = Enum.TryParse(UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value, out Roles role) ? role : Roles.None

				};
			}
			return null;
		}

        public string Login(UserLogin value)
        {
            var user = Authenticate(value);	
            if (user != null)
            {
                var token = Generate(user);
                return token;
            }
            return "user not found";
        }
    }
}
