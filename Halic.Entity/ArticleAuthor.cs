using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class ArticleAuthor
    {
        public int ArticleId { get; set; }
        public Article Articles { get; set; }
        public int AuthorId { get; set; }
        public Author Authors { get; set; }
    }
}
