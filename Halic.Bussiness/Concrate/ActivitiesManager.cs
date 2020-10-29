using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class ActivitiesManager:IActivitiesServices
    {
        private IActivitiesRepository _activitiesRepository;
        public ActivitiesManager(IActivitiesRepository activitiesrepository)
        {
            _activitiesRepository = activitiesrepository;
        }

        public void Create(Activities Entity)
        {
            _activitiesRepository.Create(Entity);
        }

        public void Delete(Activities Entity)
        {
            _activitiesRepository.Delete(Entity);
        }

        public Activities GetActivitiesDetails(string url)
        {
           return _activitiesRepository.GetActivitiesDetails(url);
        }

        public List<Activities> GetAll()
        {
            return _activitiesRepository.GetAll();
        }

        public Activities GetById(int id)
        {
            return _activitiesRepository.GetById(id);
        }

        public List<Activities> GetOrderAll()
        {
            return _activitiesRepository.GetOrderAll();
        }

        public void Update(Activities Entity)
        {
            _activitiesRepository.Update(Entity);
        }
    }
}
