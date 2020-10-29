using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class NCategoryManager : INCategoryServices
    {
        private INCategoryRepository _NCategoryRepository;
        public NCategoryManager(INCategoryRepository Ncategoryrepository)
        {
            _NCategoryRepository = Ncategoryrepository;
        }
        public void Create(NCategory Entity)
        {
            _NCategoryRepository.Create(Entity);
        }

        public void Delete(NCategory Entity)
        {
            _NCategoryRepository.Delete(Entity);
        }

        public List<NCategory> GetAll()
        {
           return  _NCategoryRepository.GetAll();
        }

        public NCategory GetById(int id)
        {
            return _NCategoryRepository.GetById(id);
        }

        public void Update(NCategory Entity)
        {
            _NCategoryRepository.Update(Entity);
        }
    }
}
