using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface IActivitiesServices
    {
        Activities GetById(int id);
        List<Activities> GetAll();
        void Create(Activities Entity);
        void Update(Activities Entity);
        void Delete(Activities Entity);
        Activities GetActivitiesDetails(string url);
        List<Activities> GetOrderAll();

    }
}
