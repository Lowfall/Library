using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.UOW.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private ApplicationDbContext context;
        public AuthorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAll(int page)
        {
            return await context.Authors.Skip(page*10).Take(10).ToListAsync();
        }

        public async Task<Author> Get(int id)
        {
            return await context.Authors.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public void Add(Author author)
        {
            context.Authors.AddAsync(author);
        }
        public void Delete(int id)
        {
            var author = context.Authors.Find(id);
            if(author == null) throw new Exception("There is no author with id " + id);
            else context.Authors.Remove(author);
        }

        public void Update(Author author)
        {
            context.Authors.Update(author);
        }

        public async Task<Author> GetByNameAndSurname(string name, string surname)
        {
            return await context.Authors.Where(a => a.Name == name && a.Surname == surname).FirstOrDefaultAsync();
        }

        public bool Exists(int id)
        {
            return context.Authors.Any(a => a.Id == id);
        }
    }
}
