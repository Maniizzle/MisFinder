using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ILostItemRepository lostrepository;
        private readonly IFoundItemRepository foundrepository;
        private readonly IFoundItemClaimRepository claimRepository;
        private readonly ILostItemClaimRepository lostItemClaimRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeetingRepository meetingRepository;

        public DashboardController(ILostItemRepository lostrepository,
            IFoundItemRepository foundrepository,
            IFoundItemClaimRepository claimRepository,
            ILostItemClaimRepository lostItemClaimRepo,
            UserManager<ApplicationUser> userManager,
            IMeetingRepository meetingRepository)
        {
            this.lostrepository = lostrepository;
            this.foundrepository = foundrepository;
            this.claimRepository = claimRepository;
            this.lostItemClaimRepo = lostItemClaimRepo;
            this.userManager = userManager;
            this.meetingRepository = meetingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lostItem = await lostrepository.GetFilterLostItems().CountAsync();
            var founditem = await foundrepository.GetFilterFoundItems().CountAsync();
            var foundClaim = await claimRepository.GetFilterFoundItemClaims().CountAsync();
            var lostClaim = await lostItemClaimRepo.GetFilterLostItemClaims().CountAsync();
            var meeting = await meetingRepository.GetAllMeetings().Where(c => c.Status == MeetingStatus.Scheduled).CountAsync();
            var activelostClaims = await lostItemClaimRepo.GetFilterLostItemClaims().Where(c => c.Status == ClaimStatus.Valid).CountAsync();
            var activefoundclaims = await claimRepository.GetFilterFoundItemClaims().Where(c => c.Status == ClaimStatus.Valid).CountAsync();
            var activeClaims = activefoundclaims + activelostClaims;
            return View(new DashIndexViewModel { LostItemsCount = lostItem, FoundItemCount = founditem, FOundItemClaimCount = foundClaim, LostItemClaimCount = lostClaim, ActiveClaimCount = activeClaims, ScheduledMeetingCount = meeting });
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

            var lostItems = lostrepository.GetFilterLostItems().Where(c => c.IsDeleted == false);
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
            int pageSize = 2;
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

            var foundItems = foundrepository.GetFilterFoundItems().Where(c => c.IsDeleted == false);
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
            //ViewBag.DeleteSortParm = sortOrder == "True" ? "True" : "False";
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

            var foundItemClaims = claimRepository.GetFilterFoundItemClaims().Where(c => c.IsDeleted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                foundItemClaims = foundItemClaims.
                    Where(c => c.ApplicationUser.FirstName.Contains(searchString) || c.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    foundItemClaims = foundItemClaims.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;

                //case "True":
                //    foundItemClaims = foundItemClaims.OrderBy(c => c.IsDeleted);
                //    break;

                case "Date":
                    foundItemClaims = foundItemClaims.OrderBy(s => s.DateLost);
                    break;

                case "date_desc":
                    foundItemClaims = foundItemClaims.OrderByDescending(c => c.DateLost);
                    break;

                default:
                    foundItemClaims = foundItemClaims.OrderBy(c => c.ApplicationUser.FirstName);
                    break;
            }
            int pageSize = 1;
            return View(await PaginatedList<FoundItemClaim>.CreateAsync(foundItemClaims.AsNoTracking(), pageNumber ?? 1, pageSize));
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

            var lostItemClaim = lostItemClaimRepo.GetFilterLostItemClaims().Where(c => c.IsDeleted == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                lostItemClaim = lostItemClaim.
                    Where(c => c.ApplicationUser.FirstName.Contains(searchString) || c.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    lostItemClaim = lostItemClaim.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;

                case "Date":
                    lostItemClaim = lostItemClaim.OrderBy(s => s.DateFound);
                    break;

                case "date_desc":
                    lostItemClaim = lostItemClaim.OrderByDescending(c => c.DateFound);
                    break;

                default:
                    lostItemClaim = lostItemClaim.OrderBy(c => c.ApplicationUser.FirstName);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<LostItemClaim>.CreateAsync(lostItemClaim.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}