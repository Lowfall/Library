using AutoMapper;
using Library.Application.DTO;
using Library.Infrastructure.Interfaces;
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
        public IActionResult GetBook()
        {
            var book = unitOfWork.Books.Get(1).Result;
            var bookdto = mapper.Map<BookDTO>(book);

            return Ok(bookdto);
        }
    }
}
