using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface ISliderServices
    {
        Slider GetById(int id);
        List<Slider> GetAll();
        void Create(Slider Entity);
        void Update(Slider Entity);
        void Delete(Slider Entity);
        List<Slider> GetOrderAll();

    }
}
