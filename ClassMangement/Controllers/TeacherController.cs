using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeacherController : ControllerBase
	{
		private readonly IService<TeacherDto,string> service;
        private readonly ISecurity<TeacherDto, UserLogin> securityService;
        private readonly IConfiguration config;

        public TeacherController(IService<TeacherDto, string> service, IConfiguration config, ISecurity<TeacherDto, UserLogin> securityService)
        {
            this.service = service;
            this.config = config;
            this.securityService = securityService;
        }

        // GET: api/<TeacherController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Roles.Master)}")]
        public List<TeacherDto> Get()
        {
			return service.GetAll();
		}

		// GET api/<TeacherController>/5
		[HttpGet("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Master)}")]
		public TeacherDto Get(string id)
		{
			return service.GetById(id);
		}

		// POST api/<TeacherController>
		[HttpPost]
        public TeacherDto Post([FromBody] TeacherDto value)
		{
			return service.AddItem(value);
		}
		[HttpPost("login")]
		public string Login([FromBody] UserLogin value)
		{
			return securityService.Login(value);
		}

        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
        public IActionResult Put(string id, [FromBody] TeacherDto value)
        {
            string userId = securityService.GetCurrentUser().Id;
            Roles userRole = securityService.GetCurrentUser().Role;

            if (userRole == Roles.Admin && userId != id)
            {
                return Forbid(); 
            }

            TeacherDto updated = service.UpdateItem(id, value);

            if (updated == null)
                return NotFound(); 

            return Ok(updated); 
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
        public IActionResult Delete(string id)
		{
            string userId = securityService.GetCurrentUser().Id;
            Roles userRole = securityService.GetCurrentUser().Role;

            if (userRole == Roles.Admin && userId != id)
            {
                return Forbid();
            }

            TeacherDto deleted = service.DeleteItem(id);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
		}
	}
}
