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
        public BookController( IBookService bookService)
        {
            this.bookService = bookService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = bookService.GetAll();

            return Ok(books);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetBookById([FromQuery]int id)
        {
            try { 
                var book = bookService.GetById(id);
                return Ok(book);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetBookByISBN([FromQuery]string isbn)
        {
            try { 
            var book = bookService.GetByISBN(isbn);
            return Ok(book);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddBook([FromBody]BookDTO bookModel)
        {
            try
            {
                bookService.Add(bookModel);
                return Ok("Book added");
            }
            catch(BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateBook([FromBody] BookDTO bookModel, [FromQuery]int id)
        {
            try
            {
                bookService.Update(bookModel,id);
                return Ok("Book added");
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteBook([FromQuery]int id)
        {
            try { 
                bookService.Delete(id);
                return Ok("Book deleted");
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
