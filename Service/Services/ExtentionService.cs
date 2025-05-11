using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public static class ExtentionService
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{

			services.AddRepository();
			services.AddScoped<IService<StudentDto, string>, StudentService>();
			services.AddScoped<IService<TeacherDto, string>, TeacherService>();
			services.AddScoped<IService<ChairDto,int>,ChairService>();
			services.AddScoped<IService<ChairDto, int>, ChairService>();
			services.AddScoped<IService<ClassDto, int>, ClassService>();
			services.AddScoped<IService<DailyAttendanceDto, int>, DailyAttendanceService>();
			services.AddScoped<IService<MarkDto, int>, MarkService>();
			services.AddScoped<IService<SubjectDto, int>, SubjectService>();
			services.AddScoped<IService<SurveyAnswerDto, int>, SurveyAnswerService>();
			services.AddScoped<IService<SurveyDto, int>, SurveyService>();
			services.AddScoped<ISecurity<UserDto, UserLogin>, SecurityService>();

			//
			//services.AddScoped<IService<UserDto, string>, UserService>();


			//...כאן נגדיר את תלויות הservice
			services.AddAutoMapper(typeof(DtoEntityMapper));
			return services;
		}
	}
}
