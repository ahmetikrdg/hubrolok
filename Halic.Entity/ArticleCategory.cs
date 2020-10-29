using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class ArticleCategory
    {
        public int ArticleId { get; set; }
        public Article Articles { get; set; }

        public int CategoryId { get; set; }
        public Category Categories { get; set; }
    }
}