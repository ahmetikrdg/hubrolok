using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface IEMailServices
    {
        EMail GetById(int id);
        List<EMail> GetAll();
        void Create(EMail Entity);
        void Update(EMail Entity);
        void Delete(EMail Entity);
    }
}
