using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyAnswerController : ControllerBase
    {
        private readonly IService<SurveyAnswerDto, int> service;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly ISecurity<Student, UserLogin> securityServiceStudent;
        private readonly IConfiguration config;

        public SurveyAnswerController(IService<SurveyAnswerDto, int> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher, ISecurity<Student, UserLogin> securityServiceStudent)
        {
            this.service = service;
            this.config = config;
            this.securityServiceTeacher = securityServiceTeacher;
            this.securityServiceStudent = securityServiceStudent;
        }

        // GET: api/<SurveyAnswerController>
        [HttpGet]
        public List<SurveyAnswerDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<SurveyAnswerController>/5
        [HttpGet("{id}")]
        public SurveyAnswerDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<SurveyAnswerController>
        [HttpPost]
        public SurveyAnswerDto Post([FromBody] SurveyAnswerDto value)
        {
            return service.AddItem(value);
        }

        // PUT api/<SurveyAnswerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SurveyAnswerDto value)
        {
            //
            SurveyAnswerDto updated = service.UpdateItem(id, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<SurveyAnswerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            //

            SurveyAnswerDto deleted = service.DeleteItem(id);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
