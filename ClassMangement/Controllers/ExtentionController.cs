using ClassMangement.Interfaces;
using ClassMangement.Security;
using Common.Dto;
using Service.Interfaces;
using Service.Services;

namespace ClassMangement.Controllers
{
    public static class ExtentionController
    {
        public static IServiceCollection AddExtentionControllers(this IServiceCollection services)
        {

            services.AddServices();
            //services.AddScoped<ISecurity<StudentDto, UserLogin>, StudentSecurity>();
            //services.AddScoped<ISecurity<TeacherDto, UserLogin>, TeacherSecurity>();
            //services.AddScoped<ISecurity<UserDto, UserLogin>, UserSecurity>();

			return services;
        }
    }
}
