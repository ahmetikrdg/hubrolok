using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class EMailManager : IEMailServices
    {
        private IEMailRepository _emailRepository;
        public EMailManager(IEMailRepository emailrepository)
        {
            _emailRepository = emailrepository;
        }
        public void Create(EMail Entity)
        {
            _emailRepository.Create(Entity);
        }

        public void Delete(EMail Entity)
        {
            _emailRepository.Delete(Entity);
        }

        public List<EMail> GetAll()
        {
            return _emailRepository.GetAll();
        }

        public EMail GetById(int id)
        {
            return _emailRepository.GetById(id);
        }

        public void Update(EMail Entity)
        {
            _emailRepository.Update(Entity);
        }
    }
}
