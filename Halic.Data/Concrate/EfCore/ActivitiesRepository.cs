using Halic.Data.Abstract;
using Halic.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class ActivitiesRepository : GenericRepository<Activities, HalicContext>, IActivitiesRepository
    {
        public Activities GetActivitiesDetails(string url)
        {
            using(var context=new HalicContext())
            {
                return context.Activities.Where(i => i.Url == url).FirstOrDefault();        
            }
        }

        public List<Activities> GetOrderAll()
        {
            using (var context = new HalicContext())
            {
                return context.Activities.OrderByDescending(i => i.ActivitiesId).ToList();
            }
        }
    }
}
