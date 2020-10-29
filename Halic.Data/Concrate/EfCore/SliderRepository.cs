using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class SliderRepository : GenericRepository<Slider, HalicContext>, ISliderRepository
    {
        public List<Slider> GetOrderAll()
        {
            using (var context = new HalicContext())
            {
                return context.Sliders.OrderByDescending(i => i.SliderId).ToList();
            }
        }
    }
}
