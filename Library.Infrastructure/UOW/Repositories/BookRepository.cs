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
    public class BookRepository : IBookRepository
    {
        private ApplicationDbContext context;
        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await context.Books.ToListAsync(); 
        }

        public async Task<Book> Get(int id)
        {
            return await context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(Book book)
        {
            await context.Books.AddAsync(book); 
        }
        public void Delete(Book book)
        {
            context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            context.Books.Update(book);    
        }
    }
}