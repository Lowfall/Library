using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Domain.Entities;
using Library.Infrastructure.Interfaces;
using Library.Infrastructure.UOW;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class AuthService : IAuthService
    {
        IHashService hashService;
        IUnitOfWork unitOfWork;
        IMapper mapper;
        IJwtTokenService tokenService;

        public AuthService(IHashService hashService, IUnitOfWork unitOfWork, IMapper mapper, IJwtTokenService tokenService)
        {
            this.mapper = mapper;
            this.hashService = hashService;
            this.unitOfWork = unitOfWork;
            this.tokenService = tokenService;

        }
        public void RegisterUser(UserDTO model)
        {
            if (unitOfWork.Users.Exist(model.Email))
            {
                throw new BadHttpRequestException("This user with such email is already exist");
            }

            var hash = hashService.HashPassword(model.Password, out byte[] salt);
            model.Password = hash;
            model.PasswordSalt = salt;

            unitOfWork.Users.Add(mapper.Map<User>(model));
            unitOfWork.Save();
        }

        public string AuthenticateUser(AuthenticateModel model)
        {
            var user = unitOfWork.Users.GetByEmail(model.Email).Result;
            if (user == null)
            {
                throw new BadHttpRequestException("There is no such user");
            }
            if (!hashService.VerifyPassword(model.Password, user.Password, user.PasswordSalt))
            {
                throw new Exception("Not correct password");
            }
            var claims = tokenService.GenerateClaims(user);
            var token = tokenService.GenerateToken(claims, DateTime.Now.Add(TimeSpan.FromMinutes(3)));
            return token;

        }
    }
}
