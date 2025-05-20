using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;
using System.Collections.Generic;

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IService<StudentDto, string> service;
        private readonly IStudentQueryLogic serviceQueryLogic;
        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly IConfiguration config;

        public StudentController(IService<StudentDto, string> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent, IStudentQueryLogic serviceQueryLogic)
        {
            this.service = service;
            this.config = config;
            this.securityServiceStudent = securityServiceStudent;
            this.securityServiceTeacher = securityServiceTeacher;
            this.serviceQueryLogic = serviceQueryLogic;
        }

        // GET: api/<StudentController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Roles.Master)}")]
        public async Task<ActionResult<List<StudentDto>>> Get()
        {
           List<StudentDto> students = await service.GetAll();
            return Ok(students);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<StudentDto>> Get(string id)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            //
            var studentDto = await serviceQueryLogic.GetByIdLogic(id, userRole, userId);

            if (studentDto == null)
                return NotFound();

            return Ok(studentDto);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Post([FromForm] StudentDto value)
        {
            StudentDto created = await service.AddItem(value);

            if (created == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLogin value)
        {
            string token = await securityServiceStudent.Login(value);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Roles.AuthorizedUser)},{nameof(Roles.User)}")]
        public async Task<ActionResult<StudentDto>> Put(string id, [FromBody] StudentDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            if (userId != id)
            {
                return Forbid();
            }

            StudentDto updated = await service.UpdateItem(id, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpPut("putTeacher")]
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        public async Task<ActionResult<StudentConfidentialInfoDto>> PutForTeacher(string id, [FromBody] StudentConfidentialInfoDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;

            StudentConfidentialInfoDto updated = await serviceQueryLogic.UpdateLogicForTeacher(id, userId, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<StudentDto>> Delete(string id)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            StudentDto deleted = await serviceQueryLogic.DeleteLogic(id, userRole, userId);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
