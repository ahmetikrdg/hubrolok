using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string NameSurname { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }     
        public string Image { get; set; }
        public string Url { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Order { get; set; }       
        public List<ArticleAuthor> ArticleAuthors { get; set; }
        public List<NewsAuthor> newsAuthors { get; set; }

    }
}
