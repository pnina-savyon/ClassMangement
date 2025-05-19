using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;

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
        public ActionResult<List<StudentDto>> Get()
        {
            var students = service.GetAll();
            return Ok(students);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<StudentDto> Get(string id)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            var studentDto = serviceQueryLogic.GetByIdLogic(id, userRole, userId);

            if (studentDto == null)
                return NotFound();

            return Ok(studentDto);
        }

        // POST api/<StudentController>
        [HttpPost]
        public ActionResult<StudentDto> Post([FromForm] StudentDto value)
        {
            var created = service.AddItem(value);

            if (created == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] UserLogin value)
        {
            var token = securityServiceStudent.Login(value);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Roles.AuthorizedUser)},{nameof(Roles.User)}")]
        public ActionResult<StudentDto> Put(string id, [FromBody] StudentDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            if (userId != id)
            {
                return Forbid();
            }

            var updated = service.UpdateItem(id, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpPost("putTeacher")]
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        public ActionResult<StudentConfidentialInfoDto> PutForTeacher(string id, [FromBody] StudentConfidentialInfoDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;

            var updated = serviceQueryLogic.UpdateLogicForTeacher(id, userId, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<StudentDto> Delete(string id)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;
            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            var deleted = serviceQueryLogic.DeleteLogic(id, userRole, userId);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}


//using Common.Dto;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Repository.Entities;
//using Repository.Entities.Enums;
//using Service.Interfaces;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace ClassMangement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentController : ControllerBase
//    {
//        private readonly IService<StudentDto, string> service;
//        private readonly IStudentQueryLogic serviceQueryLogic;
//        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
//        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
//        private readonly IConfiguration config;


//        public StudentController(IService<StudentDto, string> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent, IStudentQueryLogic serviceQueryLogic)
//        {
//            this.service = service;
//            this.config = config;
//            this.securityServiceStudent = securityServiceStudent;
//            this.serviceQueryLogic = serviceQueryLogic;
//        }

//        // GET: api/<StudentController>
//        [HttpGet]
//        [Authorize(Roles = $"{nameof(Roles.Master)}")]
//        public List<StudentDto> Get()
//        {      
//              return service.GetAll();
//        }

//        // GET api/<StudentController>/5
//        [HttpGet("{id}")]
//        [Authorize]
//        public StudentDto Get(string id)
//        {
//            string userId = securityServiceTeacher.GetCurrentUser().Id;
//            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

//            return serviceQueryLogic.GetByIdLogic(id, userRole, userId);
//        }

//        // POST api/<StudentController>
//        [HttpPost]
//        public StudentDto Post([FromForm] StudentDto value)
//        {
//            return service.AddItem(value);
//        }

//        [HttpPost("login")]
//        public string Login([FromBody] UserLogin value)
//        {
//             return securityServiceStudent.Login(value);
//        }

//        // PUT api/<StudentController>/5
//        [HttpPut("{id}")]
//        [Authorize(Roles = $"{nameof(Roles.AuthorizedUser)},{nameof(Roles.User)}")]
//        public IActionResult Put(string id, [FromBody] StudentDto value)
//        {
//            string userId = securityServiceTeacher.GetCurrentUser().Id;
//            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

//            if (userId != id)
//            {
//                return Forbid();
//            }

//            StudentDto updated = service.UpdateItem(id, value);

//            if (updated == null)
//                return NotFound();

//            return Ok(updated);
//        }

//        [HttpPost("putTeacher")]
//        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
//        public IActionResult PutForTeacher(string id, [FromBody] StudentConfidentialInfoDto value)
//        {
//            string userId = securityServiceTeacher.GetCurrentUser().Id;

//            StudentConfidentialInfoDto updated = serviceQueryLogic.UpdateLogicForTeacher(id, userId, value);

//            if (updated == null)
//                return NotFound();

//            return Ok(updated);
//        }

//        // DELETE api/<StudentController>/5
//        [HttpDelete("{id}")]
//        [Authorize]
//        public IActionResult Delete(string id)
//        {
//            string userId = securityServiceTeacher.GetCurrentUser().Id;
//            Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

//            StudentDto deleted = serviceQueryLogic.DeleteLogic(id, userRole, userId);

//            if (deleted == null)
//                return NotFound();

//            return Ok(deleted);        
//        }
//    }
//}
