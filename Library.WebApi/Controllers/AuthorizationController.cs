using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        IAuthService authService;
        public AuthorizationController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost]
        public IActionResult Register([FromBody] UserDTO model)
        {
            authService.RegisterUser(model);
            return Ok("You registered");
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var token = authService.AuthenticateUser(model);
            return Ok("You authenticated, this is your token : " + token);
        }
    }
}
