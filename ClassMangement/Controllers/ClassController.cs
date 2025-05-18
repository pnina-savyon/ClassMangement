using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IService<ClassDto, int> service;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly IConfiguration config;

        public ClassController(IService<ClassDto, int> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent)
        {
            this.service = service;
            this.config = config;
            this.securityServiceTeacher = securityServiceTeacher;
            this.securityServiceStudent = securityServiceStudent;
        }
        // GET: api/<ClassController>
        [HttpGet]
        public List<ClassDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<ClassController>/
        [HttpGet("{id}")]
        public ClassDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<ClassController>
        [HttpPost]
        public ClassDto Post([FromBody] ClassDto value)
        {
            return service.AddItem(value);
        }

        // PUT api/<ClassController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ClassDto value)
        {
            ClassDto updated = service.UpdateItem(id, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<ClassController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ClassDto deleted = service.DeleteItem(id);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
