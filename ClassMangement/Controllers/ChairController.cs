using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairController : ControllerBase
    {
        private readonly IService<ChairDto, int> service;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly IQueryLogicGeneric<ChairDto, int> serviceQueryLogicGeneric;
        private readonly IServiceChair serviceChair;
        private readonly IConfiguration config;

        public ChairController(IService<ChairDto, int> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher,
            ISecurity<Student, UserLogin> securityServiceStudent,IQueryLogicGeneric<ChairDto, int> serviceQueryLogicGeneric, IServiceChair serviceChair)
        {
            this.service = service;
            this.config = config;
            this.securityServiceTeacher = securityServiceTeacher;
            this.securityServiceStudent = securityServiceStudent;
            this.serviceQueryLogicGeneric = serviceQueryLogicGeneric;
            this.serviceChair = serviceChair;
        }
        // GET: api/<ChairController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Roles.Master)}")]
        public async Task<ActionResult<List<ChairDto>>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<ChairController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ChairDto>> Get(int id)
        {
            User? teacherUser = securityServiceTeacher.GetCurrentUser();
            User? studentUser = securityServiceStudent.GetCurrentUser();
            User? userDto = teacherUser ?? studentUser;

            string userId = userDto.Id;
            Roles userRole = userDto.Role;

            ChairDto chairDto = await serviceQueryLogicGeneric.GetByIdLogic(id, userRole, userId);

            if (chairDto == null)
                return NotFound();

            return Ok(chairDto);
        }

        [HttpGet("AllChairsOfClass/{classId}")]
        [Authorize]
        public async Task<ActionResult<List<ChairDto>>> GetAllChairsOfClass(int classId)
        {
            User? teacherUser = securityServiceTeacher.GetCurrentUser();
            User? studentUser = securityServiceStudent.GetCurrentUser();
            User? userDto = teacherUser ?? studentUser;

            string userId = userDto.Id;
            Roles userRole = userDto.Role;

            List<ChairDto> chairsDto = await serviceChair.AllChairsOfClass(classId, userRole, userId);
            if (chairsDto == null)
                 return NotFound();

            return Ok(chairsDto);
        }

        // POST api/<ChairController>
        [HttpPost]
        [Authorize(Roles = $"{nameof(Roles.Master)} ,{nameof(Roles.Admin)}")]
        public async Task<ActionResult<ChairDto>> Post([FromForm] ChairDto value)
        {
            ChairDto created = await service.AddItem(value);

            if (created == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<ChairController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        public async Task<ActionResult<ChairDto>> Put(int id, [FromForm] ChairDto value)
        {
            string userId = securityServiceTeacher.GetCurrentUser().Id;

            ChairDto updated = await serviceQueryLogicGeneric.UpdateLogic(id, userId, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<ChairController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(Roles.Master)} ,{nameof(Roles.Admin)}")]
        public async Task<ActionResult<ChairDto>> Delete(int id)
        {
            User user = securityServiceTeacher.GetCurrentUser();    
            string userId = user.Id;
            Roles userRole = user.Role;

            ChairDto updated = await serviceQueryLogicGeneric.DeleteLogic(id, userRole, userId);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
    }
}
