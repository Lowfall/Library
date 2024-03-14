using Library.Application.DTO;
using Library.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IAuthorService : IModelService<AuthorDTO>
    {
    }
}
