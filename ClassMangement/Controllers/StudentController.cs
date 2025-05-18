using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IService<StudentDto, string> service;
        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly IConfiguration config;


        public StudentController(IService<StudentDto, string> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent)
        {
            this.service = service;
            this.config = config;
            this.securityServiceStudent = securityServiceStudent;
        }

        // GET: api/<StudentController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Roles.Master)}")]
        public List<StudentDto> Get()
        {      
              return service.GetAll();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        [Authorize]
        public StudentDto Get(string id)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

 
            if(((userRole == Roles.User  || userRole == Roles.AuthorizedUser) && userId != id) || userRole == Roles.None)
            {
                return null;
            }
            if(userRole == Roles.Admin)
            {
                //if (//bool function - אם תלמיד שייך למורה )
                    //return
            }

            return service.GetById(id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost("login")]
        public string Login([FromBody] UserLogin value)
        {
            return securityServiceStudent.Login(value);
        }


        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Roles.AuthorizedUser)},{nameof(Roles.User)}")]
        public IActionResult Put(string id, [FromBody] StudentDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            if (userId != id)
            {
                return Forbid();
            }

            StudentDto updated = service.UpdateItem(id, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpPost("putTeacher")]
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        public IActionResult Put(string id, [FromBody] StudentConfidentialInfoDto value)
        {
            //if (//bool function - אם תלמיד שייך למורה )
            //return

        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = $"{nameof(Roles.Admin)}")]
        public IActionResult Delete(string id)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            if (userId != id)
            {
                return Forbid();
            }

            StudentDto deleted = service.DeleteItem(id);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
