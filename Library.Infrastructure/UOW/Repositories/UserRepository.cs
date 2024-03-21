using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.UOW.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAll(int page)
        {
            return await context.Users.Skip(page * 10).Take(10).ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await context.Users.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await context.Users.Where(b => b.Email == email).FirstOrDefaultAsync();
        }

        public void Add(User user)
        {
            context.Users.AddAsync(user);
        }

        public void Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user == null) throw new Exception("There is no user with id " + id);
            else context.Users.Remove(user);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }

        public bool Exist(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public bool Exists(int id)
        {
            return context.Users.Any(b => b.Id == id);
        }
    }
}
