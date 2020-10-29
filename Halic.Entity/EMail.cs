using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Halic.Entity
{
    public class EMail
    {
        public int Id { get; set; }

        public string NameSurname { get; set; }
        [Required]
        public string Email { get; set; }
    }       
}
