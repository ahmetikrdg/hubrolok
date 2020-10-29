using Halic.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class NewsTakeViewComponent : ViewComponent
    {
        private INewsServices _newsServices;    

        public NewsTakeViewComponent(INewsServices newsServices)
        {
            this._newsServices = newsServices;
        }
        public IViewComponentResult Invoke()
        {

            var model = new HalicHub.Models.NewsListViewModel
            {
                pageInfo = null,
                News = _newsServices.GetAllToTake()
            };
            return View(model);
        }
    }
}
