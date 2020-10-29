using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class SliderModel
    {
        public int SliderId { get; set; }
        [Display(Name = "Başlık", Prompt = "Başlık adını giriniz")]
        public string Tittle { get; set; }
        [Display(Name = "Özet", Prompt = "Özet giriniz")]
        public string Description { get; set; }
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Display(Name = "Url", Prompt = "İlgili yazının linkini kopyalayıp buraya yapıştırınız")]
        public string Url { get; set; }
        
    }
}
