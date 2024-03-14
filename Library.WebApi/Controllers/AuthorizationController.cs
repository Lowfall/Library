using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        IHashService hashService;
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public AuthorizationController(IHashService hashService, IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Register([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter correct data");
            }

            if (unitOfWork.Users.Exist(model.Email))
            {
                return BadRequest("This user with such email is already exist");
            }

            var hash = hashService.HashPassword(model.Password, out byte[] salt);

            model.Password = hash;
            model.PasswordSalt = salt;

            unitOfWork.Users.Add(mapper.Map<User>(model));
            unitOfWork.Save();

            return Ok("You registered");
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = unitOfWork.Users.GetByEmail(model.Email).Result;
            if (user == null)
            {
                return BadRequest("There is no such user");
            }
            if (hashService.VerifyPassword(model.Password,user.Password,user.PasswordSalt))
            {
                return Ok("It works");
            }
            return Ok("You authenticated, this is your token : ");
        }
    }
}
