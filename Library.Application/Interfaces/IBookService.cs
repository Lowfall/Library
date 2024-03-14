using Library.Application.DTO;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBookService : IModelService<BookDTO>
    {
       BookDTO GetByISBN(string isbn);
        void Update(BookDTO obj, int id);
    }
}
