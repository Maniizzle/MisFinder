using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Pagination;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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

        [HttpPost, ActionName("Deletes")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            if (id == null)
            { return NotFound(); }
            var user = await userManager.FindByIdAsync(id);
            user.IsBlackListed = true;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}