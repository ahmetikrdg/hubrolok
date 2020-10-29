using Halic.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private ICategoryServices _categoryServices;

        public CategoriesViewComponent(ICategoryServices categoryServices)
        {
            this._categoryServices = categoryServices;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["category"] != null)
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_categoryServices.GetAll());
        }
    }
}
