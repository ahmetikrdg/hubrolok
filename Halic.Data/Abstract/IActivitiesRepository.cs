using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface IActivitiesRepository:IRepository<Activities>
    {
        Activities GetActivitiesDetails(string url);
        List<Activities> GetOrderAll();

    }
}
