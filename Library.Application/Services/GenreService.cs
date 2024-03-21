using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
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
    public class GenreService : IGenreService
    {

        IUnitOfWork unitOfWork;
        IMapper mapper;
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public void Add(GenreDTO obj)
        {
            var genre = unitOfWork.Genres.GetByName(obj.Name).Result;
            if(genre != null)
            {
                throw new BadHttpRequestException($"Genre {obj.Name} is already exists");
            }
            unitOfWork.Genres.Add(mapper.Map<Genre>(obj));
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            unitOfWork.Genres.Delete(id);
            unitOfWork.Save();
        }

        public async Task<IEnumerable<GenreDTO>> GetAll()
        {
            var genres = await unitOfWork.Genres.GetAll();
            if (genres == null)
            {
                throw new BadHttpRequestException("There is no genres");
            }
            return  mapper.Map<List<GenreDTO>>(genres);
        }

        public async Task<IEnumerable<GenreDTO>> GetAll(int page)
        {
            var genres = await unitOfWork.Genres.GetAll(page);
            if (genres == null)
            {
                throw new BadHttpRequestException("There is no genres");
            }
            return mapper.Map<List<GenreDTO>>(genres);
        }

        public async Task<GenreDTO> GetById(int id)
        {
            var genre = await unitOfWork.Genres.Get(id);
            if (genre == null)
            {
                throw new BadHttpRequestException("There is no genre with id " + id);
            }
            return mapper.Map<GenreDTO>(genre);
        }
    }
}
