using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username = configuration["Data:ServerUser:username"];
            var Password = configuration["Data:ServerUser:Password"];
            var EMail = configuration["Data:ServerUser:EMail"];
            var Role = configuration["Data:ServerUser:Role"];

            if (await userManager.FindByNameAsync(username)==null)
            {
                await roleManager.CreateAsync(new IdentityRole(Role));

                var user = new User()
                {
                    UserName=username,
                    Email=EMail,
                    FirstName="Server",
                    LastName="Server",
                };
                var result = await userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role);   
                }
            }


        }
    }
}
