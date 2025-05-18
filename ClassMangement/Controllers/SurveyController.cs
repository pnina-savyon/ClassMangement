using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassMangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IService<SurveyDto, int> service;
        private readonly ISecurity<Teacher, UserLogin> securityServiceTeacher;
        private readonly IConfiguration config;

        public SurveyController(IService<SurveyDto, int> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher)
        {
            this.service = service;
            this.config = config;
            this.securityServiceTeacher = securityServiceTeacher;
        }


        // GET: api/<SurveyController>
        [HttpGet]
        public List<SurveyDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<SurveyController>/5
        [HttpGet("{id}")]
        public SurveyDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<SurveyController>
        [HttpPost]
        public SurveyDto Post([FromBody] SurveyDto value)
        {
            return service.AddItem(value);
        }

        // PUT api/<SurveyController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SurveyDto value)
        {
            //string userId = securityServiceTeacher.GetCurrentUser().Id;
            //Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            //if (userRole == Roles.Admin && userId != id)
            //{
            //    return Forbid();
            //}
            SurveyDto updated = service.UpdateItem(id, value);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE api/<SurveyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //string userId = securityServiceTeacher.GetCurrentUser().Id;
            //Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

            //if (userRole == Roles.Admin && userId != id)
            //{
            //    return Forbid();
            //}

            SurveyDto deleted = service.DeleteItem(id);

            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
