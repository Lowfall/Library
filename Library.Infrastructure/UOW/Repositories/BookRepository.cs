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
    public class BookRepository : IBookRepository
    {
        private ApplicationDbContext context;
        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await context.Books.Include(b => b.Author).Include(g=>g.Genre).ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAll(int page)
        {
            return await context.Books.Include(b => b.Author).Include(g => g.Genre).Skip(page * 10).Take(10).ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await context.Books.Include(b => b.Author).Include(g => g.Genre).Where(b => b.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            return await context.Books.Include(b => b.Author).Include(g => g.Genre).Where(b => b.ISBN == isbn).FirstOrDefaultAsync();
        }

        public void Add(Book book)
        {
           context.Books.AddAsync(book); 
        }
        public void Delete(int id)
        {
            var book = context.Books.Find(id);
            if (book == null) throw new Exception("There is no book with id " + id);
            else context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            context.Books.Update(book);    
        }

        public bool Exists(int id)
        {
            return context.Books.Any(b => b.Id == id);
        }
    }
}
