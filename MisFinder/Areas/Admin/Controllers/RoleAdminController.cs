using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleMgr;
            this.userManager = userManager;
        }

        public IActionResult Index() => View(roleManager.Roles);

        //  public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View("Index", roleManager.Roles);
        }

        //public async Task<IActionResult> Edit(string id)
        //{
        //    IdentityRole role = await roleManager.FindByIdAsync(id);
        //    List<ApplicationUser> members = new List<ApplicationUser>();
        //    List<ApplicationUser> nonMembers = new List<ApplicationUser>();
        //    foreach (ApplicationUser user in userManager.Users)
        //    {
        //        var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
        //        list.Add(user);
        //    }
        //    return View(new RoleEditModel
        //    {
        //        Role = role,
        //        Members = members,
        //        NonMembers = nonMembers
        //    });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(RoleModificationModel model)
        //{
        //    IdentityResult result;
        //    if (ModelState.IsValid)
        //    {
        //        foreach (string userId in model.IdsToAdd ?? new string[] { })
        //        {
        //            ApplicationUser user = await userManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                result = await userManager.AddToRoleAsync(user, model.RoleName);
        //                if (!result.Succeeded)
        //                {
        //                    AddErrorsFromResult(result);
        //                }
        //            }
        //        }

        //        foreach (string userId in model.IdsToDelete ?? new string[] { })
        //        {
        //            ApplicationUser user = await userManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
        //                if (!result.Succeeded)
        //                {
        //                    AddErrorsFromResult(result);
        //                }
        //            }
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else { return await Edit(model.RoleId); }
        //}
        public async Task<IActionResult> AssignRole()
        {
            IEnumerable<IdentityRole> roles = await roleManager.Roles.ToListAsync();

            ViewBag.Roles = roles;
            IEnumerable<UserViewModel> users = await userManager.Users.Select(c => new UserViewModel { Email = c.Email, Id = c.Id }).ToListAsync();
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "UserName doesnt Exist");
                    return View();
                }
                await userManager.AddToRoleAsync(user, model.Id);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([Bind]string Id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}