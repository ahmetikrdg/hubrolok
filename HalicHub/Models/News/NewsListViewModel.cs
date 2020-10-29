using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class NewsListViewModel
    {
        public PageInfoNews pageInfo { get; set; }

        public List<News> News { get; set; }
    }
}
