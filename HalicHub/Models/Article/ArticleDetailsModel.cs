using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class ArticleDetailsModel
    {
        public Article Articles { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public List<Article> Articlescategory { get; set; }

    }
}
    