using Halic.Bussiness.Abstract;
using HalicHub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class VideoFirstViewComponent : ViewComponent
    {
        private IVideoServices _videoServices;

        public VideoFirstViewComponent(IVideoServices videoServices)
        {
            this._videoServices = videoServices;
        }
        public IViewComponentResult Invoke()
        {

            return View(new VideoListViewModel
            {
                Videos = _videoServices.GetOrderOneAll()
            });
        }
    }
    
}
