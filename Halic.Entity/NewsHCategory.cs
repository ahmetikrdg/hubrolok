using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class NewsHCategory
    {
        public int NCategoryId { get; set; }
        public NCategory NCategories { get; set; }          

        public int NewsId { get; set; }
        public News News { get; set; }  
    }
}
