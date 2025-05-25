using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IService<StudentDto, string> service;
        private readonly IQueryLogicForFewFunctions<StudentDto,string> serviceQueryLogicForFewFunctions;
		private readonly IQueryLogicUpdate<StudentConfidentialInfoDto, string> serviceQueryLogicUpdate;
		private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly IServiceStudent serviceStudent;
        private readonly IConfiguration config;

        public StudentController(IService<StudentDto, string> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent,
			IQueryLogicUpdate<StudentConfidentialInfoDto, string> serviceQueryLogicUpdate, IQueryLogicForFewFunctions<StudentDto, string> serviceQueryLogicForFewFunctions, IServiceStudent serviceStudent)
        {
            this.service = service;
            this.config = config;
            this.securityServiceStudent = securityServiceStudent;
            this.securityServiceTeacher = securityServiceTeacher;
            this.serviceQueryLogicUpdate = serviceQueryLogicUpdate;
            this.serviceQueryLogicForFewFunctions = serviceQueryLogicForFewFunctions;
            this.serviceStudent = serviceStudent;
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
            User? teacherUser = securityServiceTeacher.GetCurrentUser();
            User? studentUser = securityServiceStudent.GetCurrentUser();
            User? userDto = teacherUser ?? studentUser;

            string userId = userDto.Id;
            Roles userRole = userDto.Role;

            //
            StudentDto studentDto = await serviceQueryLogicForFewFunctions.GetByIdLogic(id, userRole, userId);

            if (studentDto == null)
                return NotFound();

            return Ok(studentDto);
        }

        [HttpGet("AllStudentsOfClass/{classId}")]
        [Authorize]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudentsOfClass(int classId)
        {
            User? teacherUser = securityServiceTeacher.GetCurrentUser();
            User? studentUser = securityServiceStudent.GetCurrentUser();
            User? userDto = teacherUser ?? studentUser;

            string userId = userDto.Id;
            Roles userRole = userDto.Role;

            List<StudentDto> studentsDto = await serviceStudent.AllStudentsOfClass(classId, userRole, userId);
            if (studentsDto == null)
                return NotFound();

            return Ok(studentsDto);
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
            User user = securityServiceStudent.GetCurrentUser();
            string userId = user.Id;
            Roles userRole = user.Role;

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

            StudentConfidentialInfoDto updated = await serviceQueryLogicUpdate.UpdateLogic(id, userId, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<StudentDto>> Delete(string id)
        {
            User user = securityServiceTeacher.GetCurrentUser();
            string userId = user.Id;
            Roles userRole = user.Role;

            StudentDto deleted = await serviceQueryLogicForFewFunctions.DeleteLogic(id, userRole, userId);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
