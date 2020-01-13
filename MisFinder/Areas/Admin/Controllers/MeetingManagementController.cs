using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Notification.Email;
using MisFinder.Data.Pagination;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MeetingManagementController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IFoundItemClaimRepository foundItemClaimRepository;
        private readonly IEmailNotifier emailSender;
        private readonly ILostItemClaimRepository lostItemClaimRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeetingRepository management;

        public MeetingManagementController(RoleManager<IdentityRole> roleMgr,
            IFoundItemClaimRepository foundItemClaimRepository, IEmailNotifier emailSender,
            ILostItemClaimRepository lostItemClaimRepository, UserManager<ApplicationUser> userManager, IMeetingRepository management)
        {
            this.roleManager = roleMgr;
            this.foundItemClaimRepository = foundItemClaimRepository;
            this.emailSender = emailSender;
            this.lostItemClaimRepository = lostItemClaimRepository;
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
                    meeting = meeting.OrderBy(c => c.IsUserTwoSelectedDate);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Meeting>.CreateAsync(meeting.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> UpdateResult(int? id, MeetingStatus status)
        {
            if (id == null || id == 0 || status == 0)
                return RedirectToAction("Index");
            var meeeting = await management.GetMeetingById(id);
            if (meeeting == null)
                return RedirectToAction("Index");

            if (status == MeetingStatus.Pending)
                meeeting.Status = MeetingStatus.Pending;
            if (status == MeetingStatus.Rescheduled)
                meeeting.Status = MeetingStatus.Rescheduled;
            if (status == MeetingStatus.Scheduled)
                meeeting.Status = MeetingStatus.Scheduled;
            if (status == MeetingStatus.Succeessful)
            {
                meeeting.Status = MeetingStatus.Succeessful;
                if (meeeting.FoundItem != null)
                {
                    meeeting.FoundItem.IsClaimed = true;
                    meeeting.FoundItem.IsMeetingSucceess = true;
                    var claim = await foundItemClaimRepository.GetFoundItemClaimsbyFoundItemId(meeeting.FoundItem.Id);
                    var claims = claim.Where(c => c.Status == ClaimStatus.Valid && c.IsAdminValid == true);
                    foreach (var item in claims)
                    {
                        item.Status = ClaimStatus.Successful;
                    }
                    foundItemClaimRepository.Save();
                }
                if (meeeting.LostItem != null)
                {
                    meeeting.LostItem.IsFound = true;
                    meeeting.LostItem.IsMeetingSucceess = true;
                    var claim = await lostItemClaimRepository.GetLostItemClaimsbyLostItemId(meeeting.LostItem.Id);//.GetFoundItemClaimsbyFoundItemId(meeeting.FoundItem.Id);
                    var claims = claim.Where(c => c.Status == ClaimStatus.Valid && c.IsAdminValid == true);
                    foreach (var item in claims)
                    {
                        item.Status = ClaimStatus.Successful;
                    }
                    lostItemClaimRepository.Save();
                }
            }
            management.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var meeting = await management.GetMeetingById(id);
            if (meeting.FoundItem != null)
                meeting = await management.GetMeetingByIdD(id);

            if (meeting.LostItem != null)
                meeting = await management.GetMeetingByIdD(id);

            return View(meeting);
        }

        [HttpPost]
        public async Task<IActionResult> SetMeetingVenue(int? meetingid, int? id, string option, string message)
        {
            if (option == "venue")
            {
                var meeting = await management.GetMeetingById(meetingid);
                if (meeting == null)
                    return NotFound();

                meeting.MeetingVenue = message;
                meeting.Status = MeetingStatus.Scheduled;
                management.Save();

                var foundItemClaim = await foundItemClaimRepository.GetFoundItemClaimById(id);
                List<string> email = new List<string>();
                email.Add(foundItemClaim.ApplicationUser.Email);
                email.Add(foundItemClaim.FoundItem.FoundItemUser.Email);
                var link = Url.Action("InviteAdmin", "Meeting", new { area = "User", id = meeting.Id }, Request.Scheme);

                var messages = new Dictionary<string, string>
            {
                {"FName",$"Users"},
                     {"UName",message },
                {"EmailClaimLink",link }
            };
                await emailSender.SendManyEmailAsync(email, "Meeting Venue", messages, "MeetingVenue");
                List<string> emails = new List<string>();
            }

            if (option == "contactuser")
            {
                var foundItemClaim = await foundItemClaimRepository.GetFoundItemClaimById(id);
                List<string> email = new List<string>();
                email.Add(foundItemClaim.ApplicationUser.Email);
                email.Add(foundItemClaim.FoundItem.FoundItemUser.Email);

                var messages = new Dictionary<string, string>
            {
                {"FName",$"Users"},
                     {"UName",message },
                //{"EmailClaimLink",link }
            };
                await emailSender.SendManyEmailAsync(email, "Inbox", messages, "contactusers");
            }
            return RedirectToAction("Details", new { id = meetingid });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var meeting = management.GetMeetingByIdD(id);

            return View(meeting);
        }

        public async Task<IActionResult> Edit(Meeting meeting)
        {
            return View();
        }
    }
}