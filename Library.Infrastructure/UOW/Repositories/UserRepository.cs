﻿using Library.Domain.Entities;
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

        public async Task<User> Get(int id)
        {
            return await context.Users.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(User user)
        {
            await context.Users.AddAsync(user);
        }
        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }
    }
}