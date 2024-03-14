using AutoMapper;
using Library.Application.DTO;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;
        public BookController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        
           
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = unitOfWork.Books.GetAll().Result.ToList();
            if(books == null)
            {
                return BadRequest("There is no books");
            }
            return Ok(mapper.Map<List<BookDTO>>(books));
        }

        [HttpGet]
        public IActionResult GetBookById([FromQuery]int id)
        {
            var book = unitOfWork.Books.Get(id).Result;
            if(book == null)
            {
                return BadRequest("There is no such book with id " + id);
            }
            return Ok(mapper.Map<BookDTO>(book));
        }

        [HttpGet]
        public IActionResult GetBookByISBN([FromQuery]string isbn)
        {
            var book = unitOfWork.Books.GetByISBN(isbn).Result;
            if (book == null)
            {
                return BadRequest("There is no such book with ISBN " + isbn);
            }
            return Ok(mapper.Map<BookDTO>(book));
        }
    }
}
