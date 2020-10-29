using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class CategoryRepository : GenericRepository<Category, HalicContext>, ICategoryRepository
    {
        public List<Category> GetPopularCategory()
        {
            using (var context = new HalicContext())
            {
               return context.Categories.Take(3).ToList();
            }
        }
    }
}
