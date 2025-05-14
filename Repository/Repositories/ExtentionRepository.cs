using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
namespace Repository.Repositories
{
    public static class ExtentionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //services.AddScoped<IRepository<User, string>, UserRepository<User>>();
            services.AddScoped<IRepository<Student, string>, StudentRepository>();
            services.AddScoped<IRepository<Teacher, string>, TeacherRepository>();
            services.AddScoped<IRepository<Chair, int>, ChairRepository>();
            services.AddScoped<IRepository<Class, int>, ClassRepository>();
            services.AddScoped<IRepository<DailyAttendance, int>, DailyAttendanceRepository>();
            services.AddScoped<IRepository<Mark, int>, MarkRepository>();
            services.AddScoped<IRepository<Subject, int>, SubjectRepository>();
            services.AddScoped<IRepository<SurveyAnswer, int>, SurveyAnswerRepository>();
            services.AddScoped<IRepository<Survey, int>, SurveyRepository>();
            //services.AddScoped<IRepository<User, string>, >();

            //
            //services.AddScoped<IRepository<User, string>, UserRepository>();

            //...כאן נגדיר את כל התלויות של הrepository
            // services.AddScoped < IRepository < Product > ProductRepository > ();
            return services;
        }
    }
}
