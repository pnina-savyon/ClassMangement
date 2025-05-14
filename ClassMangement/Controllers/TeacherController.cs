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
	public class TeacherController : ControllerBase
	{
		private readonly IService<TeacherDto,string> service;
        private readonly ISecurity<TeacherDto, UserLogin> securityService;
        private readonly IConfiguration config;

        public TeacherController(IService<TeacherDto, string> service, IConfiguration config, ISecurity<TeacherDto, UserLogin> securityService)
        {
            this.service = service;
            this.config = config;
            this.securityService = securityService;
        }

        // GET: api/<TeacherController>
        [HttpGet]
		public List<TeacherDto> Get()
		{
			return service.GetAll();
		}

		// GET api/<TeacherController>/5
		[HttpGet("{id}")]
		public TeacherDto Get(string id)
		{
			return service.GetById(id);
		}

		// POST api/<TeacherController>
		[HttpPost]
		public TeacherDto Post([FromBody] TeacherDto value)
		{
			return service.AddItem(value);
		}
		[HttpPost("login")]
		public string Login([FromBody] UserLogin value)
		{
			return securityService.Login(value);
		}

		// PUT api/<TeacherController>/5
		[HttpPut("{id}")]
		public void Put(string id, [FromBody] TeacherDto value)
		{
			service.UpdateItem(id, value);
		}

		// DELETE api/<TeacherController>/5
		[HttpDelete("{id}")]
		public TeacherDto Delete(string id)
		{
			return service.DeleteItem(id);
		}
	}
}
