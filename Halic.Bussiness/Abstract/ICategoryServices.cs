using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface ICategoryServices
    {
        Category GetById(int id);
        List<Category> GetAll();
        void Create(Category Entity);
        void Update(Category Entity);
        void Delete(Category Entity);
        List<Category> GetPopularCategory();

    }
}
