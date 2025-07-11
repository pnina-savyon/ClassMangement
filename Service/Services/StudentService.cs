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
    public class StudentService : UserService<Student, StudentDto>, IQueryLogicUpdate<StudentConfidentialInfoDto,string>,
        IQueryLogicForFewFunctions<StudentDto,string>, IServiceStudent
    {
        private readonly IRepositoryAllById<Student, int> repositoryAllById;

        //service מכיר גם את common וגם , ריפוזיטורי?
        //לשנות את סדר הכרת השכבות - קומון, ריפו, סרביס
        public StudentService(IRepository<Student, string> repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration config, IRepositoryAllById<Student, int> repositoryAllById)
            : base(repository, httpContextAccessor, mapper, config)
        {
            this.repositoryAllById = repositoryAllById;
        }
        private async Task<bool> IsStudentBelongsToTeacher(string idStudent, string userId)
        {
            Student s = await repository.GetById(idStudent);
            if (s == null || s.Class == null || s.Class.Teacher == null)
                return false;
            Teacher t = s.Class.Teacher;
            if (t.Id != userId)
                return false;
            return true;
        }
        private async Task<bool> IsSpecificAuthorization(string id, Roles role, string userId)
        {
            if (((role == Roles.User || role == Roles.AuthorizedUser) && userId != id) || role == Roles.None)
                return false;

            if (role == Roles.Admin && !await IsStudentBelongsToTeacher(id, userId))
                return false;

            return true;
        }
        private async Task UploadImage(IFormFile file)
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, "Images");

            // ודא שהתיקייה קיימת
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public override async Task<StudentDto> AddItem(StudentDto item)
        {
            if (item.fileImage != null)
                await UploadImage(item.fileImage);
            return mapper.Map<Student, StudentDto>(await repository.AddItem(mapper.Map<StudentDto, Student>(item)));
        }
        public override async Task<StudentDto> DeleteItem(string id)
        {
            return mapper.Map<Student, StudentDto>(await repository.DeleteItem(id));
        }

        public async Task<StudentDto> DeleteLogic(string id, Roles role, string userId)
        {
            if (!await IsSpecificAuthorization(id, role, userId))
                return null;

            return await DeleteItem(id);
        }

        public override async Task<List<StudentDto>> GetAll()
        {
            return mapper.Map<List<Student>, List<StudentDto>>(await repository.GetAll());
        }

        public override async Task<StudentDto> GetById(string id)
        {
            return mapper.Map<Student, StudentDto>(await repository.GetById(id));
        }

        public async Task<StudentDto> GetByIdLogic(string id, Roles role, string userId)
        {
            if (!await IsSpecificAuthorization(id, role, userId))
                return null;

            return await GetById(id);
        }

        public override async Task<StudentDto> UpdateItem(string id, StudentDto item)
        {
            return mapper.Map<Student, StudentDto>(await repository.UpdateItem(id, mapper.Map<StudentDto, Student>(item)));
        }


        public async Task<StudentConfidentialInfoDto> UpdateLogic(string id, string userId, StudentConfidentialInfoDto value)
        {
            if (!await IsStudentBelongsToTeacher(id, userId))
                return null;

			return mapper.Map<Student, StudentConfidentialInfoDto>(await repository.UpdateItem(id, mapper.Map<StudentConfidentialInfoDto, Student>(value)));

		}

        public async Task<List<StudentDto>> AllStudentsOfClass(int classId, Roles role, string userId)
        {
            List<Student> students = await repositoryAllById.GetAllItemOfId(classId);

            if (!students.Any())
                return null;

            Class cls = students.First().Class;
            if (cls == null)
                return null;

            bool hasAccess = role switch
            {
                Roles.Master => true,
                Roles.Admin => cls.TeacherId == userId,
                Roles.User => cls.Students?.Any(s => s.Id == userId) == true,
                Roles.AuthorizedUser => cls.Students?.Any(s => s.Id == userId) == true,
                _ => false // כל רול אחר שלא ידוע – אין גישה
            };

            return hasAccess ? mapper.Map<List<StudentDto>>(students) : null;
        }
    }
}
