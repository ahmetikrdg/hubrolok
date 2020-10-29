using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class ArticleTakeViewComponent : ViewComponent
    {
        private IArticleServices _ArticleServices;    

        public ArticleTakeViewComponent(IArticleServices ArticleServices)
        {
            this._ArticleServices = ArticleServices;
        }
        public IViewComponentResult Invoke()
        {

            var model = new HalicHub.Models.ArticleListViewModel
            {
                pageInfo = null,
                Articles = _ArticleServices.GetAllToTake()
            };
            return View(model);
        }
    }
}
