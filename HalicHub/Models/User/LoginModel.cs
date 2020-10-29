using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name ="Kullanıcı Adı",Prompt ="Kullanıcı adınızı giriniz")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Şifre", Prompt = "Şifernizi giriniz")]
        public string Password { get; set; }
    }
}
