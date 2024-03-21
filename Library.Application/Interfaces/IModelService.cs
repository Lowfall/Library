using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IModelService<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(int page);
        Task<T> GetById(int id);
        void Add(T obj);
        void Delete(int id);    
    }
}
