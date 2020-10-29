using Halic.Bussiness.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.ViewComponents
{
    public class ActivitiesViewComponent : ViewComponent
    {
        private IActivitiesServices _activitiesServices;    

        public ActivitiesViewComponent(IActivitiesServices activitiesServices)
        {
            this._activitiesServices = activitiesServices;
        }
        public IViewComponentResult Invoke()
        {

            var model = new HalicHub.Models.ActivitiesListViewModel
            {
                activities = _activitiesServices.GetAll()
            };
            return View(model);
        }
    }
    
}
