using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class NewsAuthor
    {
        public int NewsId { get; set; }
        public News News { get; set; }
        public int AuthorId { get; set; }   
        public Author Author { get; set; }
    }
}
