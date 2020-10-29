using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class VideoModel
    {
        public int VideoId { get; set; }
        [Display(Name = "Başık", Prompt = "Başlık adını giriniz")]
        public string Tittle { get; set; }
        [Display(Name = "Url", Prompt = "Url adını giriniz")]
        public string Url { get; set; }
        [Display(Name = "Tarih", Prompt = "Tarih giriniz")]
        public string Date { get; set; }
        [Display(Name = "Resim", Prompt = "Resim giriniz")]
        public string Image { get; set; }   
    }
}
