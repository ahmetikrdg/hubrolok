using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class NCategory
    {
        public int NCategoryId { get; set; }
        public string Tittle { get; set; }      
        public string Url { get; set; }
        public string Image { get; set; }
        public List<NewsHCategory> newsHCategories { get; set; }
    }
}
