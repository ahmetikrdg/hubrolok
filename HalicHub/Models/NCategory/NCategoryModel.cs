using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class NCategoryModel
    {
        public int NCategoryId { get; set; }
        [Display(Name = "Kategori Adı", Prompt = "Kategori adını giriniz")]
        public string Tittle { get; set; }
        [Display(Name = "Url", Prompt = "Url adını giriniz")]
        public string Url { get; set; }
        [Display(Name = "İcon", Prompt = "İcon ekleyiniz")]
        public string Image { get; set; }       
    }
}
