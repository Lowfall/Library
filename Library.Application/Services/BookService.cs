using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BookDTO> GetById(int id)
        {
            var book = await unitOfWork.Books.Get(id);
            if (book == null)
            {
                throw new BadHttpRequestException("There is no such book with id " + id);
            }
            return mapper.Map<BookDTO>(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            var books = await unitOfWork.Books.GetAll();
            if (books == null)
            {
                throw new BadHttpRequestException("There is no books");
            }
            return mapper.Map<List<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> GetAll(int page)
        {
            var books = await unitOfWork.Books.GetAll(page);
            if (books == null)
            {
                throw new BadHttpRequestException("There is no books");
            }
            if (books.Count() == 0)
            {
                throw new BadHttpRequestException("There is no books");
            }
            return mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> GetByISBN(string isbn)
        {
            var book = await unitOfWork.Books.GetByISBN(isbn);
            if (book == null)
            {
                throw new BadHttpRequestException("There is no such book with ISBN " + isbn);
            }
            return mapper.Map<BookDTO>(book);
        }

        public void Add(BookDTO obj)
        {
            if (!unitOfWork.Genres.Exists(obj.GenreId))
            {
                throw new BadHttpRequestException("No genre with such id");
            }
            if (!unitOfWork.Authors.Exists(obj.AuthorId))
            {
                throw new BadHttpRequestException("No author with such id");
            }

            var book = mapper.Map<Book>(obj);
            book.ReturnDateTime = book.TakeDateTime + TimeSpan.FromDays(30);
            unitOfWork.Books.Add(book);
            unitOfWork.Save();
        }
   

        public void Update(BookDTO obj, int id)
        {

            if (!unitOfWork.Books.Exists(id))
            {
                throw new BadHttpRequestException("No book with such id");
            }
            if (!unitOfWork.Genres.Exists(obj.GenreId))
            {
                throw new BadHttpRequestException("No genre with such id");
            }
            if (!unitOfWork.Authors.Exists(obj.AuthorId))
            {
                throw new BadHttpRequestException("No author with such id");
            }

            obj.Id = id;
            var book = mapper.Map<Book>(obj);
            book.ReturnDateTime = book.TakeDateTime + TimeSpan.FromDays(30);
            unitOfWork.Books.Update(book);
            unitOfWork.Save();
        }



        public void Delete(int id)
        {
            var book = unitOfWork.context.Books.Find(id);
            if (book == null) throw new BadHttpRequestException("There is no user with id " + id);
            unitOfWork.Books.Delete(book);
            unitOfWork.Save();
        }
    }
}


