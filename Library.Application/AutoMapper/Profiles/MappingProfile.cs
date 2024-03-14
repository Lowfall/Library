using AutoMapper;
using Library.Application.DTO;
using Library.Domain.Entities;
using Library.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.AutoMapper.Profiles
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {
            CreateMap<User, UserDTO>(); 
            CreateMap<UserDTO,User> (); 
            CreateMap<Author, AuthorDTO>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<Book, BookDTO>();
            
        }
    }
}
