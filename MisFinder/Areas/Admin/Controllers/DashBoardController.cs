using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Pagination;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ILostItemRepository lostrepository;
        private readonly IFoundItemRepository foundrepository;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ILostItemRepository lostrepository,IFoundItemRepository foundrepository,
            UserManager<ApplicationUser> userManager)
        {
            this.lostrepository = lostrepository;
            this.foundrepository = foundrepository;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LostItems(string sortOrder)
        {
            var lostitems= lostrepository.GetAllLostItems();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Action = "FoundItems";
            var lostItems = lostrepository.GetFilterLostItems();
            switch (sortOrder)
            {
                case "name_desc":
                    lostItems = lostItems.OrderByDescending(c => c.NameOfLostItem);
                    break;
                case "Date":
                    lostItems = lostItems.OrderBy(s => s.DateMisplaced);
                    break;
                case "date_desc":
                    lostItems = lostItems.OrderByDescending(c => c.DateMisplaced);
                    break;
                default:
                    lostItems = lostItems.OrderBy(c => c.NameOfLostItem);
                    break;
            }
            return View(await lostItems.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> FoundItems(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Action = "FoundItems";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var foundItems = foundrepository.GetFilterFoundItems();
            if(!String.IsNullOrEmpty(searchString))
            {
                foundItems = foundItems.
                    Where(c => c.NameOfFoundItem.Contains(searchString) || c.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    foundItems = foundItems.OrderByDescending(c => c.NameOfFoundItem);
                    break;
                case "Date":
                    foundItems = foundItems.OrderBy(s => s.DateFound);
                    break;
                case "date_desc":
                    foundItems = foundItems.OrderByDescending(c => c.DateFound);
                    break;
                default:
                    foundItems = foundItems.OrderBy(c => c.NameOfFoundItem);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<FoundItem>.CreateAsync(foundItems.AsNoTracking(),pageNumber??1,pageSize));
        }
    }
}