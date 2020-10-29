using Halic.Bussiness.Abstract;
using HalicHub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class ArticleInCategoriesViewComponent:ViewComponent
    {
        private ICategoryServices _categoryServices;
        public ArticleInCategoriesViewComponent(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["category"] != null)
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_categoryServices.GetAll());
        }
    }
}



