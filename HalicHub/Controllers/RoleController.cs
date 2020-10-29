using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalicHub.Identity;
using HalicHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HalicHub.Controllers
{
    [Authorize(Roles = "server")]

    public class RoleController : Controller
    { //Rol İşlemleri

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _UserManager;
        public RoleController(RoleManager<IdentityRole> IdentityRole, UserManager<User> userManager)
        {
            _roleManager = IdentityRole;
            _UserManager = userManager;
        }

        // ---------------------> Rol OPERASYONLARI <-----------------------------
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();
            var userList = _UserManager.Users.ToList();
            foreach (var item in userList)
            {
                var list = await _UserManager.IsInRoleAsync(item, role.Name) ? members : nonmembers;
                list.Add(item);
            }
            var model = new RoleDetailModel()
            {
                Members = members,
                Role = role,
                NonMembers = nonmembers
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var UserId in model.IdsToAdd ?? new string[] { })//ilgili olan role eklenen userler idleridolaşalım
                {
                    var user = await _UserManager.FindByIdAsync(UserId);//iligli userid ile useri alalım
                    if (user != null)
                    {
                        var result = await _UserManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var item in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _UserManager.FindByIdAsync(item);
                    if (user != null)
                    {
                        var result = await _UserManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return Redirect("/Role/rolelist/" + model.RoleId);
        }

        public async Task<IActionResult> RoleDelete(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("RoleList");
        }

        // ------------------> User Operasyonları <-------------------------------

        public IActionResult UserList()
        {
            return View(_UserManager.Users);
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _UserManager.FindByIdAsync(id);
            if (user != null)
            {
                var selectedRoles = await _UserManager.GetRolesAsync(user);//rol bilg aldım
                var roles = _roleManager.Roles.Select(i => i.Name);//birden fazla name seçerse diye

                ViewBag.Roles = roles;
                return View(new UserDetailModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    SelectedRoles = selectedRoles
                });
            }
            return Redirect("~/Role/userlist");
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.UserName = model.UserName;

                    var result = await _UserManager.UpdateAsync(user);
                    var userRoles = await _UserManager.GetRolesAsync(user);//kullanıcı rollerini alalım
                    selectedRoles = selectedRoles ?? new string[] { };
                    await _UserManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                    await _UserManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                    return Redirect("/Role/userlist");

                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(string RoleId)
        {
            var user = await _UserManager.FindByIdAsync(RoleId);
            await _UserManager.DeleteAsync(user);
            return RedirectToAction("UserList");
        }

        public IActionResult RoleView()
        {

            return View();
        }



    }
}
