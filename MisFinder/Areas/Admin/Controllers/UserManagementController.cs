using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Pagination;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Action = "Index";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var users = userManager.Users;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.
                    Where(c => c.FirstName.Contains(searchString) || c.FullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(c => c.LastName);
                    break;

                default:
                    users = users.OrderBy(c => c.LastName);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<ApplicationUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpPost, ActionName("BlackList")]
        public async Task<IActionResult> BlackListUser(string id)
        {
            if (id == null)
            { return NotFound(); }
            var user = await userManager.FindByIdAsync(id);
            user.IsBlackListed = true;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index), new { pageNumber = 2 });
        }

        public async Task<IActionResult> Approve(string id)
        {
            if (id == null)
            { return NotFound(); }
            var user = await userManager.FindByIdAsync(id);
            user.IsBlackListed = false;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            //var rol=roleManager.
            var roles = await userManager.GetRolesAsync(user);
            var nroles = roles.ToList();
            //var model = new EditRoleViewModel
            //{
            //    Id = id,
            //    Roles = nroles,
            //    User = user.FullName
            //};
            return View();
        }

        //public IActionResult RemoveRole(string id, string Role)
        //{
        //}
    }
}