using Halic.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class NCategoriesViewComponent:ViewComponent
    {
        private INCategoryServices _ncategoriesServices;    

        public NCategoriesViewComponent(INCategoryServices ncategoriesServices)
        {
            this._ncategoriesServices = ncategoriesServices;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["categories"] != null)
                ViewBag.SelectedCategory = RouteData?.Values["categories"];
            return View(_ncategoriesServices.GetAll());
        }
    }
}

