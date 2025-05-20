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
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly IConfiguration config;

        public TeacherController(IService<TeacherDto, string> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher)
        {
            this.service = service;
            this.config = config;
            this.securityServiceTeacher = securityServiceTeacher;
        }

        // GET: api/<TeacherController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Roles.Master)}")]
        public async Task<ActionResult<List<TeacherDto>>> Get()
        {
            List<TeacherDto> teachers = await service.GetAll();
            return Ok(teachers);

		}

		// GET api/<TeacherController>/5
		[HttpGet("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Master)}")]
		public async Task<ActionResult<TeacherDto>> Get(string id)
		{
            TeacherDto teacherDto = await service.GetById(id);

            if(teacherDto == null)
                return NotFound();

            return Ok(teacherDto);
        }

		// POST api/<TeacherController>
		[HttpPost]
        public async Task<ActionResult<TeacherDto>> Post([FromForm] TeacherDto value)
		{
            TeacherDto created = await service.AddItem(value);

            if (created == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromBody] UserLogin value)
		{
            string token = await securityServiceTeacher.Login(value);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
        public async Task<ActionResult<TeacherDto>> Put(string id, [FromBody] TeacherDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            if (userRole == Roles.Admin && userId != id)
            {
                return Forbid(); 
            }

            TeacherDto updated = await service.UpdateItem(id, value);

            if (updated == null)
                return NotFound(); 

            return Ok(updated); 
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
        public async Task<ActionResult<TeacherDto>> Delete(string id)
		{
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            if (userRole == Roles.Admin && userId != id)
            {
                return Forbid();
            }

            TeacherDto deleted = await service.DeleteItem(id);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
		}
	}
}
