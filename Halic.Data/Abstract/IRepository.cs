using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Create(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
