using System;
using System.Collections.Generic;
using System.Linq;

//using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Notification.Email;
using MisFinder.Data.Pagination;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ClaimManagementController : Controller
    {
        private readonly IFoundItemClaimRepository foundItemClaim;
        private readonly ILostItemClaimRepository lostItemClaim;
        private readonly IEmailNotifier emailSender;

        public ClaimManagementController(IFoundItemClaimRepository foundItemClaim,
            ILostItemClaimRepository lostItemClaim, IEmailNotifier emailSender)
        {
            this.foundItemClaim = foundItemClaim;
            this.lostItemClaim = lostItemClaim;
            this.emailSender = emailSender;
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
            var claim = lostItemClaim.GetFilterLostItemClaims().Where(c => c.Status == ClaimStatus.Valid);
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

        public async Task<IActionResult> ValidatedFoundItemsClaim(string sortOrder, string currentFilter, string searchString, int? pageNumber)
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
            var claim = foundItemClaim.GetFilterFoundItemClaims().Where(c => c.Status == ClaimStatus.Valid);
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

        public async Task<IActionResult> ContactFoundFinder(string date, int? id, string name, string mail)
        {
            if (date == null || id == null || name == null)
                return NotFound();
            char[] array = date.ToCharArray();
            Array.Reverse(array);
            var newdate = new string(array);
            // date = date.Reverse().ToString();
            var link = Url.Action("SelectMeetingDate", "Meeting", new { area = "User", Num = id, dat = newdate }, Request.Scheme);
            System.IO.File.WriteAllText("Meetinglink.txt", link);

            var claim = await foundItemClaim.GetFoundItemClaimById(id);
            claim.IsAdminValid = true;
            foundItemClaim.Save();

            var message = new Dictionary<string, string>
            {
                {"FName",$"{name}"},
                {"EmailClaimLink",link }
            };
            await emailSender.SendEmailAsync(mail, "Meeting Arrangement", message, "SetUpMeeting");
            return RedirectToAction("ValidatedFoundItemsClaim", "ClaimManagement", new { area = "Admin" });
        }

        public async Task<IActionResult> ContactLostFinder(string date, int? id, string name, string mail)
        {
            if (date == null || id == null || name == null)
                return NotFound();
            char[] array = date.ToCharArray();
            Array.Reverse(array);
            var newdate = new string(array);
            // date = date.Reverse().ToString();
            var link = Url.Action("SelectMeetingDate", "Meeting", new { area = "User", Num = id, dat = newdate }, Request.Scheme);
            System.IO.File.WriteAllText("Meetinglink.txt", link);

            var claim = await lostItemClaim.GetLostItemClaimById(id);
            claim.IsAdminValid = true;
            lostItemClaim.Save();

            var message = new Dictionary<string, string>
            {
                {"FName",name},
                {"EmailClaimLink",link }
            };
            await emailSender.SendEmailAsync(mail, "Meeting Arrangement", message, "SetUpMeeting");
            return RedirectToAction("Index", "ClaimManagement", new { area = "Admin" });
        }

        public async Task<IActionResult> FoundDetails(int? id)
        {
            if (id == null)
                return NotFound();
            var claim = await foundItemClaim.GetFoundItemClaimById(id);
            if (claim == null)
                return NotFound();
            return View(claim);
        }

        public async Task<IActionResult> LostDetails(int? id)
        {
            if (id == null)
                return NotFound();
            var claim = await lostItemClaim.GetLostItemClaimById(id);
            if (claim == null)
                return NotFound();
            return View(claim);
        }
    }
}