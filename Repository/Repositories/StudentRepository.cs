using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : UserRepository<Student>
    {
        private readonly IContext context;

        public StudentRepository(IContext context)
        {
            this.context = context;
        }
        public override Student AddItem(Student item)
        {
            this.context.Students.Add(item);
            this.context.Save();
            return item;
        }

        public override Student DeleteItem(string id)
        {
            Student item = GetById(id);
            if (item != null)
            {
                this.context.Students.Remove(item);
                this.context.Save();
            }
            return item;
        }

        public override List<Student> GetAll()
        {
            return this.context.Students.ToList();
        }

        public override Student GetById(string id)
        {
            return this.context.Students
                .Include(s => s.Class)
                .ThenInclude(c => c.Teacher)
                .FirstOrDefault(s => s.Id == id);
        }

        public override Student UpdateItem(string id, Student item)
        {

            Student student = GetById(id);
            if (student == null)
                return null;
            student.Password = item.Password != null ? item.Password : student.Password;
            student.Name = item.Name != null ? item.Name : student.Name; ;
            student.Marks = item.Marks != null ? item.Marks : student.Marks;
            student.ChairId = item.ChairId != 0 ? item.ChairId : student.ChairId;
            student.Phone = item.Phone != null ? item.Phone : student.Phone;
            student.Address = item.Address != null ? item.Address : student.Address;
            student.Priority = item.Priority != 0 ? item.Priority : student.Priority;
            student.StatusSocial = item.StatusSocial != 0 ? item.StatusSocial : student.StatusSocial;
            student.FavoriteFriends = item.FavoriteFriends != null ? item.FavoriteFriends : student.FavoriteFriends;
            student.ClassId = item.ClassId != 0 ? item.ClassId : student.ClassId;
            student.Role = item.Role != 0 ? item.Role : student.Role;
            student.AttentionLevel = item.AttentionLevel != 0 ? item.AttentionLevel : student.AttentionLevel;
            student.ImageUrl = item.ImageUrl != null ? item.ImageUrl : student.ImageUrl;
            student.HistoryChairs = item.HistoryChairs != null ? item.HistoryChairs : student.HistoryChairs;
            student.DailyAttendances = item.DailyAttendances != null ? item.DailyAttendances : student.DailyAttendances;

            this.context.Save();
            return student;
        }
    }
}
