using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Entity
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }//başlık
        public string Content { get; set; }//içerik
        public string Description { get; set; }//açıklama özet
        public string Date { get; set; }//tarih
        public string Image { get; set; }//resim
        public string Url { get; set; }//urlsi
        public bool IsApproved { get; set; }
        public List<NewsHCategory> newsHCategories { get; set; }
        public List<NewsAuthor> newsAuthors { get; set; }

    }
}
