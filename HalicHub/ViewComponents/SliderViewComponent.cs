using Halic.Bussiness.Abstract;
using HalicHub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private ISliderServices _sliderServices;

        public SliderViewComponent(ISliderServices sliderServices)
        {
            this._sliderServices = sliderServices;
        }
        public IViewComponentResult Invoke()
        {

            return View(new SliderListViewModel
            {
                Sliders = _sliderServices.GetAll()
            });
        }
    }
}
