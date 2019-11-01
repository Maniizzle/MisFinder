using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Pagination;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClaimManagementController : Controller
    {
        private readonly IFoundItemClaimRepository foundItemClaim;
        private readonly ILostItemClaimRepository lostItemClaim;

        public ClaimManagementController(IFoundItemClaimRepository foundItemClaim, ILostItemClaimRepository lostItemClaim)
        {
            this.foundItemClaim = foundItemClaim;
            this.lostItemClaim = lostItemClaim;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
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
            var claim = lostItemClaim.GetFilterLostItemClaims().Where(c => c.IsValidated == true);
            if (!String.IsNullOrEmpty(searchString))
            {
                claim = claim.
                    Where(c => (c.ApplicationUser.FirstName.Contains(searchString) || c.Description.Contains(searchString)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    claim = claim.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;

                case "Date":
                    claim = claim.OrderBy(s => s.DateFound);
                    break;

                case "date_desc":
                    claim = claim.OrderByDescending(c => c.DateFound);
                    break;

                default:
                    claim = claim.OrderBy(c => c.CreatedOn);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<LostItemClaim>.CreateAsync(claim.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> ValidatedFoundItemClaims(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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
            var claim = foundItemClaim.GetFilterFoundItemClaims().Where(c => c.IsValidated == true);
            if (!String.IsNullOrEmpty(searchString))
            {
                claim = claim.
                    Where(c => (c.ApplicationUser.FirstName.Contains(searchString) || c.Description.Contains(searchString)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    claim = claim.OrderByDescending(c => c.ApplicationUser.FirstName);
                    break;

                case "Date":
                    claim = claim.OrderBy(s => s.DateLost);
                    break;

                case "date_desc":
                    claim = claim.OrderByDescending(c => c.DateLost);
                    break;

                default:
                    claim = claim.OrderBy(c => c.ValidatedOn);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<FoundItemClaim>.CreateAsync(claim.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}