using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IAuthorService authorService;
        public AuthorController(IAuthorService authorService) { 
            this.authorService = authorService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await authorService.GetAll());
        }

        [Authorize]
        [HttpGet]
        [Route("{page}")]
        public async Task<IActionResult> GetAll(int page)
        {
            return Ok(await authorService.GetAll(page));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAuthor(int id)
        {
            return Ok(await authorService.GetById(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddAuthor(AuthorDTO author)
        {
            authorService.Add(author);
            return Ok("Author added");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteAuthor(int id) 
        {
            authorService.Delete(id);
            return Ok("Author deleted");
        }
    }
}
