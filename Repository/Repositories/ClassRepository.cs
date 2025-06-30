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
    public class ClassRepository : IRepository<Class, int>
    {
        private readonly IContext context;

        public ClassRepository(IContext context)
        {
            this.context = context;
        }
        public async Task<Class> AddItem(Class item)
        {
            await this.context.Classes.AddAsync(item);
            await this.context.Save();
            return item;
        }

        public async Task<Class> DeleteItem(int id)
        {
            Class item = await GetById(id);
            this.context.Classes.Remove(item);
            await this.context.Save();
            return item;
        }

        public async Task<List<Class>> GetAll()
        {
            return await this.context.Classes
                .Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Class> GetById(int id)
        {
            return await this.context.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ThenInclude(s => s.FavoriteFriends)
                .ThenInclude(s => s.NonFavoriteFriends)
                .Include(c => c.Chairs)
                .ThenInclude(ch => ch.NearbyOfChairs)
                .ThenInclude(ch => ch.NearbyChairs)
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Class> UpdateItem(int id, Class item)
        {
            Class classItem = await GetById(id);
            classItem.Password = item.Password != null ? item.Password : classItem.Password;
            classItem.Name = item.Name != null ? item.Name : classItem.Name;
            classItem.CountOfStudents = item.CountOfStudents != 0 ? item.CountOfStudents : classItem.CountOfStudents;
            classItem.Students = item.Students != null ? item.Students : classItem.Students;
            classItem.Surveys = item.Surveys != null ? item.Surveys : classItem.Surveys;
            classItem.TeacherId = item.TeacherId != null ? item.TeacherId : classItem.TeacherId;
            classItem.Chairs = item.Chairs != null ? item.Chairs : classItem.Chairs;
            await this.context.Save();
            return classItem;
        }
    }
}
