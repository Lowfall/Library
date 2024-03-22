using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(int page);
        int GetAmount();
        void Add(T entity);
        void Delete(T obj);
        void Update(T entity);
        bool Exists(int id);
    }
}
