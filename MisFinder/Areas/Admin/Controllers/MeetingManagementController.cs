using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Pagination;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MeetingManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeetingRepository management;

        public MeetingManagementController(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userManager, IMeetingRepository management)
        {
            this.roleManager = roleMgr;
            this.userManager = userManager;
            this.management = management;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Action = "FoundItemClaims";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var meeting = management.GetAllMeetings();
            //var claim = fo.Where(c => c.Status == ClaimStatus.Valid);
            if (!String.IsNullOrEmpty(searchString))
            {
                //    meeting = meeting.
                //        Where(c => (c.FoundItemFoundItem.FirstName.Contains(searchString) || c.Description.Contains(searchString)));
            }
            switch (sortOrder)
            {
                //case "name_desc":
                //    meeting = meeting.OrderByDescending(c => c..FirstName);
                //    break;

                case "Date":
                    meeting = meeting.OrderBy(s => s.CreatedOn);
                    break;

                case "date_desc":
                    meeting = meeting.OrderByDescending(c => c.CreatedOn);
                    break;

                default:
                    meeting = meeting.OrderBy(c => c.Status);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Meeting>.CreateAsync(meeting.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}