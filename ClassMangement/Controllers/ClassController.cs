using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;
using Service.SeatAllocation.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IService<ClassDto, int> service;
        private readonly IQueryLogicGeneric<ClassDto, int> serviceQueryLogicGeneric;
        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
		private readonly ISolver solverInlayStudentsInSeats;

		private readonly IConfiguration config;

        public ClassController(IService<ClassDto, int> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent,
            IQueryLogicGeneric<ClassDto, int> serviceQueryLogicGeneric, ISolver solverInlayStudentsInSeats )
        {
            this.service = service;
            this.config = config;
            this.securityServiceStudent = securityServiceStudent;
            this.securityServiceTeacher = securityServiceTeacher;
            this.serviceQueryLogicGeneric = serviceQueryLogicGeneric;
			this.solverInlayStudentsInSeats = solverInlayStudentsInSeats;

		}
        // GET: api/<ClassController>
        [HttpGet]
        [Authorize(Roles = $"{nameof(Roles.Master)}")]
        public async Task<ActionResult<List<ClassDto>>> Get()
        {
            List<ClassDto> classes = await service.GetAll();
            return Ok(classes);

        }
        // GET api/<ClassController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClassDto>> Get(int id)
        {
            User? teacherUser = securityServiceTeacher.GetCurrentUser();
            User? studentUser = securityServiceStudent.GetCurrentUser();
            User? userDto = teacherUser ?? studentUser;

            string userId = userDto.Id;
            Roles userRole = userDto.Role;

            ClassDto classDto = await serviceQueryLogicGeneric.GetByIdLogic(id, userRole, userId);

            if (classDto == null)
                return NotFound();

            return Ok(classDto);
        }

        // POST api/<ClassController>
        [HttpPost]
        [Authorize(Roles = $"{nameof(Roles.Master)} ,{nameof(Roles.Admin)}")]
        public async Task<ActionResult<ClassDto>> Post([FromForm] ClassDto value)
        {
            ClassDto created = await service.AddItem(value);

            if (created == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<ClassController>/5
        [HttpPut("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Admin)}")]
		public async Task<ActionResult<ClassDto>> Put(int id, [FromBody] ClassDto value)
        {
			string userId = securityServiceTeacher.GetCurrentUser().Id;

			ClassDto updated = await serviceQueryLogicGeneric.UpdateLogic(id,userId, value);

			if (updated == null)
				return NotFound();

			return Ok(updated);
		}

        // DELETE api/<ClassController>/5
        [HttpDelete("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Admin)}")]
		public async Task<ActionResult<ClassDto>> Delete(int id)
        {
            User user = securityServiceTeacher.GetCurrentUser();
            string userId = user.Id;
            Roles userRole = user.Role;

            ClassDto updated = await serviceQueryLogicGeneric.DeleteLogic(id, userRole, userId);

			if (updated == null)
				return NotFound();

			return Ok(updated);
		}
    }
}
