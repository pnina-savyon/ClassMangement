using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository.Entities;
using Repository.Entities.Enums;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StudentService : UserService<Student, StudentDto>, IStudentQueryLogic
    {
        //service מכיר גם את common וגם , ריפוזיטורי?
        //לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
        public StudentService(IRepository<Student, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration config)
            : base(repository, httpContextAccessor, mapper, config)
        {

        }
        public bool IsStudentBelongsToTeacher(string idStudent, string userId)
        {
            Student s = repository.GetById(idStudent);
            if (s == null || s.Class == null || s.Class.Teacher == null)
                return false;
            Teacher t = s.Class.Teacher;
            if (t.Id != userId)
                return false;
            return true;
        }
        public bool IsSpecificAuthorization(string id, Roles role, string userId)
        {
            if (((role == Roles.User || role == Roles.AuthorizedUser) && userId != id) || role == Roles.None)
                return false;

            if (role == Roles.Admin && !IsStudentBelongsToTeacher(id, userId))
                return false;

            return true;
        }
        public override StudentDto AddItem(StudentDto item)
        {
            return mapper.Map<Student, StudentDto>(repository.AddItem(mapper.Map<StudentDto, Student>(item)));
        }

        public override StudentDto DeleteItem(string id)
        {
            return mapper.Map<Student, StudentDto>(repository.DeleteItem(id));
        }

        public StudentDto DeleteLogic(string id, Roles role, string userId)
        {
            if (!IsSpecificAuthorization(id, role, userId))
                return null;
     
            return DeleteItem(id);
        }

        public override List<StudentDto> GetAll()
        {
            return mapper.Map<List<Student>, List<StudentDto>>(repository.GetAll());
        }

        public override StudentDto GetById(string id)
        {
            return mapper.Map<Student, StudentDto>(repository.GetById(id));
        }

        public StudentDto GetByIdLogic(string id, Roles role, string userId)
        {
            if (!IsSpecificAuthorization(id, role, userId))
                return null;

            return GetById(id);
        }

        public override StudentDto UpdateItem(string id, StudentDto item)
        {
            return mapper.Map<Student, StudentDto>(repository.UpdateItem(id, mapper.Map<StudentDto, Student>(item)));
        }

        public StudentConfidentialInfoDto UpdateItemForTeacher(string id, StudentConfidentialInfoDto item)
        {
            return mapper.Map<Student, StudentConfidentialInfoDto>(repository.UpdateItem(id, mapper.Map<StudentConfidentialInfoDto, Student>(item)));
        }

        public StudentConfidentialInfoDto UpdateLogicForTeacher(string id, string userId, StudentConfidentialInfoDto value)
        {
            if (!IsStudentBelongsToTeacher(id, userId))
                return null;

            return UpdateItemForTeacher(id,value);
        }
    }
}
