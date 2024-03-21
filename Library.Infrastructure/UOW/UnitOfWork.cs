using Library.Infrastructure.Data;
using Library.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context {  get;}
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
        public IGenreRepository Genres { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(ApplicationDbContext context, IBookRepository books, IAuthorRepository authors, IGenreRepository genres, IUserRepository users)
        {
            this.context = context;
            this.Books = books;
            this.Authors = authors;
            this.Genres = genres;
            this.Users = users;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
