using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<Genre> GetByName(string name);
    }
}
