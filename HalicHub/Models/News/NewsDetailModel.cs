using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class NewsDetailModel
    {
        public News News { get; set; }
        public List<Author> Authors { get; set; }
        public List<NCategory> Categories { get; set; }
    }
}
