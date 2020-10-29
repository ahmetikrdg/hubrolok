using HalicHub.Identity;
using HalicHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalicHub.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }       
        [HttpGet]


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
                    
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu Kullanıcı Adı ile Daha önce Hesap Oluşturumadı");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");

            return View();
        }

        [HttpGet]
        public IActionResult Registerqrsfreqpxi()
        {   
            return View();
        }       
        [HttpPost]
        public async Task<IActionResult> Registerqrsfreqpxi(RegisterModel model)          
        {       
            if(!ModelState.IsValid)//istediğim şartları sağlamıyorsa
            {
                return View(model);//geri gönder
            }
            var user = new User()
            { 
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName=model.UserName,
                Email=model.EMail        
            };

            var result =await _userManager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                //generate token
                //e mail gönder
                return RedirectToAction("UserList", "Role");
            }
            ModelState.AddModelError("", "Lütfen tekrar deneyiniz");
            return View(model);
        }

        public async Task<IActionResult> Logout()       
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

       
    }
}
