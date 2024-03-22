using Library.Application.AutoMapper.Profiles;
using Library.Application.Interfaces;
using Library.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IHashService,HashService>();
            services.AddScoped<IJwtTokenService,TokenService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JwtSettings:ISSUER"],
                        ValidAudience = config["JwtSettings:AUDIENCE"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:KEY"]))
                    };
                });
            services.AddAuthorization();
            return services;
        }
    }
}
