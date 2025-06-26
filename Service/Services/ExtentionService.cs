using Common.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Solver;
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
			services.AddScoped<ISecurity<Teacher, UserLogin>, SecurityService<Teacher,TeacherDto>>();
            services.AddScoped<ISecurity<Student, UserLogin>, SecurityService<Student, StudentDto>>();

			//
            services.AddScoped<IQueryLogicUpdate<StudentConfidentialInfoDto,string>, StudentService>();
			services.AddScoped<IQueryLogicForFewFunctions<StudentDto,string>, StudentService>();
            services.AddScoped<IServiceStudent, StudentService>();
            services.AddScoped<IServiceChair, ChairService>();



            services.AddScoped<IQueryLogicGeneric<ClassDto, int>, ClassService>();
            services.AddScoped<IQueryLogicGeneric<ChairDto, int>, ChairService>();

            services.AddScoped<ISolver, Solver>();
            services.AddScoped<ISeatingAllocationInputValidator,SeatingAllocationInputValidator>();



            //services.AddScoped<ISecurity<UserDto, UserLogin>, SecurityService>();

            //services.AddScoped<ISecurity<UserDto, UserLogin>, SecurityService<User, UserDto>>();


            //
            //services.AddScoped<IService<UserDto, string>, UserService>();

            //...כאן נגדיר את תלויות הservice
            services.AddAutoMapper(typeof(DtoEntityMapper));
			return services;
		}
	}
}
