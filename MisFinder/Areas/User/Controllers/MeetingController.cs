using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Notification.Email;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisFinder.Areas.User.Controllers
{
    [Area("User")]
    public class MeetingController : Controller
    {
        private readonly IFoundItemRepository repository;
        private readonly IFoundItemClaimRepository claimRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailNotifier emailNotifier;
        private readonly IStateRepository stateRepository;
        private readonly IMeetingRepository meetingRepository;

        public MeetingController(IFoundItemRepository repository, IFoundItemClaimRepository claimRepository,
            UserManager<ApplicationUser> userManager, IEmailNotifier emailNotifier, IStateRepository stateRepository, IMeetingRepository meetingRepository)
        {
            this.repository = repository;
            this.claimRepository = claimRepository;
            this.userManager = userManager;
            this.emailNotifier = emailNotifier;
            this.stateRepository = stateRepository;
            this.meetingRepository = meetingRepository;
        }

        public async Task<IActionResult> SelectMeetingDate(int? num, string dat)
        {
            if (num == null || dat == null)
                return NotFound();
            var claim = await claimRepository.GetFoundItemClaimById(num);
            var meeting = await meetingRepository.GetMeetingByFoundItemId(claim.FoundItem.Id);
            if (meeting != null)
                return NotFound();
            //reverse the date
            char[] array = dat.ToCharArray();
            Array.Reverse(array);
            var newdate = new string(array);

            if (claim == null || newdate != claim.CreatedOn.ToString())
                return NotFound();

            if (claim.FoundItem.FoundItemUser != await userManager.GetUserAsync(User))
                return NotFound();
            ViewBag.Id = claim.Id;
            ViewBag.User = claim.ApplicationUserId;
            TempData["States"]
             = await stateRepository.GetAllStates();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectMeetingDate(int? id, string user, MeetingDateViewModel meetingDate)
        {
            if (id == null || user == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                var claim = await claimRepository.GetFoundItemClaimById(id);
                if (user != claim.ApplicationUserId)
                    return View();
                var meeting = new Meeting { FoundItem = claim.FoundItem, UserSelectedDate = (DateTime)meetingDate.FirstDate, USerSelectedDate2 = (DateTime)meetingDate.SecondDate, LocalGovernmentId = meetingDate.LGAId };
                meetingRepository.Create(meeting);
                meetingRepository.Save();
                var firstdate = ((DateTime)meetingDate.FirstDate).ToShortDateString();
                var seconddate = ((DateTime)meetingDate.SecondDate).ToShortDateString();
                var link = Url.Action("ConfirmDate", "Meeting", new { area = "User", id = claim.FoundItem.Id, firstDate = firstdate, secondDate = seconddate }, Request.Scheme);
                await System.IO.File.WriteAllTextAsync("ConfirmDate.txt", link);
                var message = new Dictionary<string, string>
                {
                    {"FName"," User"},
                    {"EmailClaimLink",link }
                };
                await emailNotifier.SendEmailAsync("olamideonakoya1@gmail.com", "Meeting Arrangement", message, "SetUpMeeting");
                return RedirectToAction("Success", "Account");
            }
            return RedirectToAction("SelectMeetingDate");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDate(int? id, string firstDate, string secondDate)
        {
            if (id == null || firstDate == null || secondDate == null)
                return NotFound();
            //  var claimuser = await claimRepository.GetFilterFoundItemClaims().Where(c => c.FoundItemId == id && c.Status == ClaimStatus.Valid).ToListAsync();
            //  claimuser
            var meeting = await meetingRepository.GetMeetingByFoundItemId(id);
            if (meeting.SelectedCount > 0)
                return NotFound();
            if (id == null || ((DateTime)meeting.UserSelectedDate).ToShortDateString() != firstDate || ((DateTime)meeting.USerSelectedDate2).ToShortDateString() != secondDate)
                return NotFound();

            return View(meeting);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDate(int id, string firstDate, string secondDate, string none = "")
        {
            if (id == 0)
                return NotFound();
            var meeting = await meetingRepository.GetMeetingById(id);
            if (meeting == null)
                return NotFound();
            meeting.SelectedCount += 1;
            if (firstDate != null)
            {
                meeting.IsSelectFirstDate = true;
                meeting.MeeetingTime = meeting.UserSelectedDate;
            }
            if (secondDate != null)
            {
                meeting.IsSelectSecondDate = true;
                meeting.MeeetingTime = meeting.USerSelectedDate2;
            }
            meeting.DateUserTwoSelectedDate = DateTime.Now;
            meetingRepository.Save();
            return RedirectToAction("Success", "Account");
        }

        public async Task<IActionResult> InviteAdmin(int? id)
        {
            if (id == null)
                return NotFound();
            var meeting = await meetingRepository.GetMeetingById(id);
            if (meeting == null)
                return NotFound();
            meeting.SelectedCount += 1;
            meeting.IsAdminIncluded = true;
            meetingRepository.Save();
            return RedirectToAction("Success", "Account");
        }
    }
}