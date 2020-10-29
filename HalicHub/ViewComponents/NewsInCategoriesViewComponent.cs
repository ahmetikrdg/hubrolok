using Halic.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class NewsInCategoriesViewComponent:ViewComponent
    {
        private INCategoryServices _ncategoryServices;
        public NewsInCategoriesViewComponent(INCategoryServices ncategoryServices)
        {
            _ncategoryServices = ncategoryServices;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["categories"] != null)
                ViewBag.SelectedCategory = RouteData?.Values["categories"];
            return View(_ncategoryServices.GetAll());
        }
    }
}
