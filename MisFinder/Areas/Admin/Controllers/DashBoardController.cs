using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Pagination;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ILostItemRepository lostrepository;
        private readonly IFoundItemRepository foundrepository;
        private readonly IFoundItemClaimRepository claimRepository;
        private readonly ILostItemClaimRepository lostItemClaimRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ILostItemRepository lostrepository,
            IFoundItemRepository foundrepository,
            IFoundItemClaimRepository claimRepository,
            ILostItemClaimRepository lostItemClaimRepo,
            UserManager<ApplicationUser> userManager)
        {
            this.lostrepository = lostrepository;
            this.foundrepository = foundrepository;
            this.claimRepository = claimRepository;
            this.lostItemClaimRepo = lostItemClaimRepo;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var lostItem = await lostrepository.GetFilterLostItems().CountAsync();
            var founditem = await foundrepository.GetFilterFoundItems().CountAsync();
            var foundClaim = await claimRepository.GetFilterFoundItemClaims().CountAsync();
            var lostClaim = await lostItemClaimRepo.GetFilterFoundItemClaims().CountAsync();

            return View(new DashIndexViewModel { LostItemsCount = lostItem, FoundItemCount = founditem, FOundItemClaimCount = foundClaim, LostItemClaimCount = lostClaim });
        }

        public async Task<IActionResult> LostItems(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var lostitems = lostrepository.GetAllLostItems();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Action = "LostItems";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var lostItems = lostrepository.GetFilterLostItems();
            if (!String.IsNullOrEmpty(searchString))
            {
                lostItems = lostItems.
                    Where(c => c.NameOfLostItem.Contains(searchString) || c.Description.Contains(searchString));
            }
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
            int pageSize = 1;
            return View(await PaginatedList<LostItem>.CreateAsync(lostItems.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> FoundItems(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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
            if (!String.IsNullOrEmpty(searchString))
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
            int pageSize = 1;
            return View(await PaginatedList<FoundItem>.CreateAsync(foundItems.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> FoundItemClaims(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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

            var foundItems = claimRepository.GetFilterFoundItemClaims();
            if (!String.IsNullOrEmpty(searchString))
            {
                foundItems = foundItems.
                    Where(c => c.ApplicationUser.FirstName.Contains(searchString) || c.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    foundItems = foundItems.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;

                case "Date":
                    foundItems = foundItems.OrderBy(s => s.DateLost);
                    break;

                case "date_desc":
                    foundItems = foundItems.OrderByDescending(c => c.DateLost);
                    break;

                default:
                    foundItems = foundItems.OrderBy(c => c.ApplicationUser.FirstName);
                    break;
            }
            int pageSize = 1;
            return View(await PaginatedList<FoundItemClaim>.CreateAsync(foundItems.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> LostItemClaims(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.Action = "LostItemClaims";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var foundItems = lostItemClaimRepo.GetFilterFoundItemClaims();
            if (!String.IsNullOrEmpty(searchString))
            {
                foundItems = foundItems.
                    Where(c => c.ApplicationUser.FirstName.Contains(searchString) || c.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    foundItems = foundItems.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;

                case "Date":
                    foundItems = foundItems.OrderBy(s => s.DateFound);
                    break;

                case "date_desc":
                    foundItems = foundItems.OrderByDescending(c => c.DateFound);
                    break;

                default:
                    foundItems = foundItems.OrderBy(c => c.ApplicationUser.FirstName);
                    break;
            }
            int pageSize = 1;
            return View(await PaginatedList<LostItemClaim>.CreateAsync(foundItems.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}