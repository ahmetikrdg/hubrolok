using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class AuthorDetailModel
    {
        public Author Authors { get; set; }
        public List<Article> Articles { get; set; }
        public List<Category> Categories { get; set; }
        public List<News> News { get; set; }


    }
}
