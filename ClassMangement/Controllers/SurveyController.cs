using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Entities.Enums;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// אין אבטחה כי לא החלטתי מי יכול להפעיל כל פונקציה
// יש סיכוי שצריך לשנות את 	SurveyDto 
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
		[Authorize(Roles = $"{nameof(Roles.Master)}")]
		public async Task<ActionResult<List<SurveyDto>>> Get()
		{
			List<SurveyDto> survey = await service.GetAll();
			return Ok(survey);
		}
		
		// האם תלמיד שישך לכיתה לא יכול לראות את הסקר?
		// GET api/<SurveyController>/5
		[HttpGet("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
		public async Task<ActionResult<SurveyDto>> Get(int id)
		{
			SurveyDto surveyDto = await service.GetById(id);

			if (surveyDto == null)
				return NotFound();

			return Ok(surveyDto);
		}

		// POST api/<SurveyController>
		[HttpPost]
		[Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
		public async Task<ActionResult<SurveyDto>> Post([FromForm] SurveyDto value)
		{
			SurveyDto created = await service.AddItem(value);

			if (created == null)
				return BadRequest();

			return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
		}

		// רק לכיתה סקר שישך למורה
		// PUT api/<SurveyController>/5
		[HttpPut("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
		public async Task<ActionResult<SurveyDto>> Put(int id, [FromBody] SurveyDto value)
		{
			string userId = securityServiceTeacher.GetCurrentUser().Id;
			Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

			//בדיקה שזה אפשרי

			SurveyDto updated = await service.UpdateItem(id, value);

			if (updated == null)
				return NotFound();

			return Ok(updated);
		}

		// כמו עידכון
		// DELETE api/<SurveyController>/5
		[HttpDelete("{id}")]
		[Authorize(Roles = $"{nameof(Roles.Master)},{nameof(Roles.Admin)}")]
		public async Task<ActionResult<SurveyDto>> Delete(int id)
		{
			string userId = securityServiceTeacher.GetCurrentUser().Id;
			Roles userRole = securityServiceTeacher.GetCurrentUser().Role;

			//בדיקה שזה אפשרי

			SurveyDto deleted = await service.DeleteItem(id);

			if (deleted == null)
				return NotFound();

			return Ok(deleted);
		}
	}
}
