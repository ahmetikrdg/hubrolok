using Halic.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }
        [Display(Name = "Başlık", Prompt = "Başlık adını giriniz")]
        public string Title { get; set; }//başlık
        [Display(Name = "İçerik", Prompt = "Makalenin içeriğini giriniz")]
        public string Content { get; set; }//içerik
        [Display(Name = "Özet", Prompt = "Makalenin özetini giriniz")]
        public string Description { get; set; }//açıklama özet
        [Display(Name = "Tarih", Prompt = "Tarih giriniz")]         
        public string Date { get; set; }//tarih
        [Display(Name = "Resim", Prompt = "Resim ekleyiniz")]
        public string Image { get; set; }//resim
        [Display(Name = "Url", Prompt = "Url adını giriniz")]
        public string Url { get; set; }//urlsi
        [Display(Name = "Onay Alanı", Prompt = "Onay")]
        public bool IsApproved { get; set; }
        public List<Category> Categories { get; set; }      
        public int CategoryId { get; set; }
        public List<ArticleCategory> ArticleCategories { get; set; }
        public List<Author> Authors { get; set; }
        public int AuthorId { get; set; }       
        public List<ArticleAuthor> ArticleAuthors { get; set; }


    }
}
