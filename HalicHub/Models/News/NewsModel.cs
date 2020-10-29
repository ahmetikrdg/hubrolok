using Halic.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class NewsModel
    {
        public int NewsId { get; set; }
        [Display(Name = "Başlık", Prompt = "Başlık adını giriniz")]
        public string Title { get; set; }
        [Display(Name = "Özet", Prompt = "Özet giriniz")]
        public string Description { get; set; }
        [Display(Name = "İçerik", Prompt = "İçerik giriniz")]
        public string Content { get; set; }
        [Display(Name = "Tarih", Prompt = "Tarih giriniz")]
        public string Date { get; set; }
        [Display(Name = "Resim")]       
        public string Image { get; set; }
        [Display(Name = "Url", Prompt = "Url adını giriniz")]
        public string Url { get; set; }
        [Display(Name = "Onay Alanı", Prompt = "Onay")]
        public bool IsApproved { get; set; }
        public List<NCategory> nCategories { get; set; }
        public int NCategoryId { get; set; }        
        public List<NewsHCategory> newsHCategories { get; set; }
        public List<NewsAuthor> NewsAuthors { get; set; }   
        public List<Author> Authors { get; set; }
        public int AuthorId { get; set; }       
    }
}
