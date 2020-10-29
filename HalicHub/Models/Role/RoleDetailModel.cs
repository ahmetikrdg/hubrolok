using HalicHub.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Models
{
    public class RoleDetailModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }//ROLE AİT KULLANICILAR
        public IEnumerable<User> NonMembers { get; set; }   //ROLE AİT OLMAYAN

    }
}
