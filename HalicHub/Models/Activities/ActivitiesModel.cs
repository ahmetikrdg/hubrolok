using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class ActivitiesModel
    {
        public int ActivitiesId { get; set; }
        [Display(Name = "Başlık", Prompt = "Başlık adını giriniz")]
        public string Tittle { get; set; }
        [Display(Name = "Özet", Prompt = "Etkinliğin özetini giriniz")]
        public string Description { get; set; }
        [Display(Name = "İçerik", Prompt = "Etkinliğin içeriğini giriniz")]
        public string Content { get; set; }
        [Display(Name = "Resim", Prompt = "Resim ekleyiniz")]
        public string Image { get; set; }
        [Display(Name = "Tarih", Prompt = "Tarih giriniz")]
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Display(Name = "Url", Prompt = "Url adını giriniz")]
        [Required(ErrorMessage = "Url adı zorunlu bir alandır")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

    }
}
