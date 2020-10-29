using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface INCategoryServices
    {
        NCategory GetById(int id);
        List<NCategory> GetAll();
        void Create(NCategory Entity);
        void Update(NCategory Entity);
        void Delete(NCategory Entity);
    }
}
