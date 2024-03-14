using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IModelService<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T obj);
        void Delete(int id);    
    }
}
