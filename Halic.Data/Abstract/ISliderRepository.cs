using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface ISliderRepository : IRepository<Slider>
    {
        List<Slider> GetOrderAll();

    }
}
