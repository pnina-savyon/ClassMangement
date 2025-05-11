using ClassMangement.Interfaces;
using Common.Dto;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities.Enums;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassMangement.Security
{
	public class TeacherSecurity : ISecurity<TeacherDto, UserLogin>
	{
		private readonly IService<TeacherDto, string> service;
		private readonly IConfiguration config;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TeacherSecurity(IService<TeacherDto, string> service, IConfiguration config, IHttpContextAccessor httpContextAccessor)
		{
			this.service = service;
			this.config = config;
			_httpContextAccessor = httpContextAccessor;

		}

		public TeacherDto Authenticate(UserLogin value)
		{
			TeacherDto user = service.GetAll().FirstOrDefault(x => x.Password == value.Password && x.Email == value.Email);
			if (user != null)
				return user;
			return null;
		}

		public string Generate(TeacherDto user)
		{
			var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
			var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
			var claims = new[] {
	new Claim(ClaimTypes.NameIdentifier,user.Name),
	new Claim(ClaimTypes.Email,user.Email),
	new Claim(ClaimTypes.PostalCode,user.Password),
    //
    new Claim(ClaimTypes.Role,user.Role.ToString()),

	};
			var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public TeacherDto GetCurrentUser()
		{
			var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
			if (identity != null)
			{
				var UserClaim = identity.Claims;
				return new TeacherDto()
				{
					Name = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
					Email = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
					Password = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.PostalCode)?.Value,
					//Role = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
					Role = Enum.TryParse(UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value, out Roles role) ? role : Roles.None

				};
			}
			return null;
		}
	}
}
