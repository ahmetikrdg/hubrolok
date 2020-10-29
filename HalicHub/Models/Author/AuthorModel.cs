using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class AuthorModel
    {
        public int AuthorId { get; set; }
        [Display(Name = "Ad Soyad", Prompt = "Ad soyad giriniz")]
        public string NameSurname { get; set; }
        [Display(Name = "Açıklama", Prompt = "Açıklama giriniz")]
        public string Description { get; set; }
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Display(Name = "Url", Prompt = "Url giriniz")]
        public string Url { get; set; }
        [Display(Name = "İçerik", Prompt = "İçerik giriniz")]
        public string Content { get; set; }
        [Display(Name = "Twitter", Prompt = "Twitter linkini giriniz")]
        public string Twitter { get; set; }
        [Display(Name = "Linkedin", Prompt = "Linkedin linkini giriniz")]
        public string Linkedin { get; set; }
        public string Order { get; set; }
    }
}
