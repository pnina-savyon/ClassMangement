using AutoMapper;
using Common.Dto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DtoEntityMapper : Profile
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Images/");

        public DtoEntityMapper()
        {
            //CreateMap<Student, StudentDto>().ForMember("ArrImage", s => s.MapFrom(x => File.ReadAllBytes(path + x.ImageUrl)));          
            //CreateMap<StudentDto, Student>().ForMember("ImageUrl", s => s.MapFrom(x => x.fileImage.FileName));
            CreateMap<Student, StudentDto>()
    .ForMember(dest => dest.ArrImage, opt =>
        opt.MapFrom(x => GetStudentImage(path, x.ImageUrl)));

            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(x => x.fileImage.FileName));
            CreateMap<Chair, ChairDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<DailyAttendance, DailyAttendanceDto>().ReverseMap();
            CreateMap<Mark, MarkDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<SurveyAnswer, SurveyAnswerDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            //?
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Student, StudentConfidentialInfoDto>().ReverseMap();



        }



        private static byte[]? GetStudentImage(string basePath, string? imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                return null;

            var fullPath = Path.Combine(basePath, imageName);
            return File.Exists(fullPath) ? File.ReadAllBytes(fullPath) : null;
        }
    }
}
