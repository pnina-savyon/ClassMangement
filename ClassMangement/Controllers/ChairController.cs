using Common.Dto;
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
        private readonly IConfiguration config;

        public ChairController(IService<ChairDto, int> service, IConfiguration config, ISecurity<Teacher, UserLogin> securityServiceTeacher)
        {
            this.service = service;
            this.config = config;
            this.securityServiceTeacher = securityServiceTeacher;
        }
        // GET: api/<ChairController>
        [HttpGet]
        public List<ChairDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<ChairController>/5
        [HttpGet("{id}")]
        public ChairDto Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<ChairController>
        [HttpPost]
        public ChairDto Post([FromBody] ChairDto value)
        {
            return service.AddItem(value);
        }

        // PUT api/<ChairController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ChairDto value)
        {
           //
            ChairDto updated = service.UpdateItem(id, value);
            if (updated == null)
                return NotFound(); 
            return Ok(updated);
        }

        // DELETE api/<ChairController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //
            ChairDto deleted = service.DeleteItem(id);
            if (deleted == null)
                return NotFound();
            return Ok(deleted);
        }
    }
}
