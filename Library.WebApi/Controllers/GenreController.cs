using Library.Application.DTO;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        IGenreService genreService;
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await genreService.GetAll());
        }

        [Authorize]
        [HttpGet]
        [Route("{page}")]
        public async Task<IActionResult> GetAll(int page)
        {
            return Ok(await genreService.GetAll(page));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGenre(int id)
        {
            return Ok(await genreService.GetById(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteGenre(int id) {
            genreService.Delete(id);
            return Ok("Genre deleted");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddGenre(GenreDTO genre)
        {
            genreService.Add(genre);
            return Ok("Genre added");
        }
    }
}
