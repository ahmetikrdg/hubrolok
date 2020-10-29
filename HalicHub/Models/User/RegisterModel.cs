using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Şifre büyük küçük harf ve *-., gibi karakterler içermelidir.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Şifre girdiğiniz şifreyle aynı olmalı")]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }



    }
}
