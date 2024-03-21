using AutoMapper;
using Library.Application.DTO;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class AuthorService : IAuthorService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public void Add(AuthorDTO obj)
        {

            unitOfWork.Authors.Add(mapper.Map<Author>(obj));
            unitOfWork.Save();
        }

        public async void Delete(int id)
        {
            unitOfWork.Authors.Delete(id);
            unitOfWork.Save();
        }

        public async Task<IEnumerable<AuthorDTO>> GetAll()
        {
            var authors = await unitOfWork.Authors.GetAll();
            if (authors == null)
            {
                throw new BadHttpRequestException("There is no authors");
            }
            return mapper.Map<List<AuthorDTO>>(authors);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAll(int page)
        {
            var authors = await unitOfWork.Authors.GetAll(page);
            if (authors == null)
            {
                throw new BadHttpRequestException("There is no authors");
            }
            return mapper.Map<List<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO> GetById(int id)
        {
            var author = await unitOfWork.Authors.Get(id);
            if (author == null)
            {
                throw new BadHttpRequestException("There is no authors with id "+ id);
            }
            return mapper.Map<AuthorDTO>(author);
        }
    }
}
