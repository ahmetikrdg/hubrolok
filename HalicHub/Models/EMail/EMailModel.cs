using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class EMailModel
    {
        public int Id { get; set; }

        public string NameSurname { get; set; } 
        [Required]
        public string Email { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }


    }
}
