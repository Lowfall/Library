using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Domain.Entities;
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
        IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await bookService.GetAll());
        }

        [Authorize]
        [HttpGet]
        [Route("{page}")]
        public async Task<IActionResult> GetAllBooks(int page)
        {
            return Ok(await bookService.GetAll(page));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBookById([FromQuery]int id)
        {
            return Ok(await bookService.GetById(id));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetBookByISBN([FromQuery]string isbn)
        {
            return Ok(await bookService.GetByISBN(isbn));
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddBook([FromBody]BookDTO bookModel)
        {
            bookService.Add(bookModel);
            return Ok("Book added");
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateBook([FromBody] BookDTO bookModel, [FromQuery]int id)
        {
            bookService.Update(bookModel,id);
            return Ok("Book added");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteBook([FromQuery]int id)
        {
            bookService.Delete(id);
            return Ok("Book deleted");
        }
    }
}
