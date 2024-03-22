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
    public class GenreRepository : IGenreRepository
    {
        private ApplicationDbContext context;
        public GenreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await context.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Genre>> GetAll(int page)
        {
            return await context.Genres.Skip((page - 1) * 10).Take(10).ToListAsync();
        }

        public async Task<Genre> Get(int id)
        {
            return await context.Genres.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public void Add(Genre genre)
        {
            context.Genres.AddAsync(genre);
        }
        public void Delete(Genre genre)
        {
            context.Genres.Remove(genre);
        }

        public void Update(Genre genre)
        {
            context.Genres.Update(genre);
        }

        public async Task<Genre> GetByName(string name)
        {
            return await context.Genres.Where(g => g.Name == name).FirstOrDefaultAsync();
        }

        public bool Exists(int id)
        {
            return context.Genres.Any(g => g.Id == id);
        }

        public int GetAmount()
        {
            return context.Genres.Count();
        }
    }
}
