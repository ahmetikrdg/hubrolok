using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface ICategoryRepository:IRepository<Category>
    {
        List<Category> GetPopularCategory();        
    }
}
