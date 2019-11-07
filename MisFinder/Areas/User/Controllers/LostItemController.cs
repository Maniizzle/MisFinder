using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Notification.Email;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MisFinder.Areas.User.Controllers

{
    [Area("User")]
    public class LostItemController : Controller
    {
        private readonly IStateRepository stateRepository;
        private readonly ILocalGovernmentRepository lgaRepository;
        private readonly IEmailNotifier emailNotifier;
        private readonly IUtility utility;
        private readonly ILostItemRepository repository;
        private readonly ILostItemClaimRepository claimRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public LostItemController(IStateRepository stateRepository,
            ILocalGovernmentRepository lgaRepository,
            IEmailNotifier emailNotifier, IUtility utility,
            ILostItemRepository repository, ILostItemClaimRepository claimRepository, UserManager<ApplicationUser> userManager)
        {
            this.stateRepository = stateRepository;
            this.lgaRepository = lgaRepository;
            this.emailNotifier = emailNotifier;
            this.utility = utility;
            this.repository = repository;
            this.claimRepository = claimRepository;
            // this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();

            IEnumerable<LostItem> lostitem = await repository.GetLostItemsByUser(user);
            return View(lostitem);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetLostItems()
        {
            IEnumerable<LostItem> lostItems = await repository.GetAllLostItems();
            return View(lostItems);
        }

        public async Task<IActionResult> LostItemById(int? id)
        {
            if (id == null)
                return NotFound();
            var lostItem = await repository.GetLostItemsById(id);
            return View(lostItem);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.StateList = await stateRepository.GetAllStates();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LostItemViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                Image image = null;
                if (model.Photo != null)
                {
                    if (!utility.IsSizeAllowed(model.Photo))
                    {
                        ModelState.AddModelError("Photo", "Your file is too large, maximum allowed size is: 5MB");
                        return View(model);
                    }

                    if (!utility.IsImageExtensionAllowed(model.Photo))
                    {
                        ModelState.AddModelError("Photo", "Please only file of type:.jpg, .jpeg, .gif, .png, .bmp  are allowed");
                        return View(model);
                    }
                    var photoPath = utility.SaveImageToFolder(model.Photo);
                    image = new Image { ImagePath = photoPath };
                }
                user.PhoneNumber = model.PhoneNumber;
                LostItem itemm = new LostItem
                {
                    LostItemUser = user,
                    NameOfLostItem = model.NameOfLostItem,
                    //StateId = model.StateId,
                    Description = model.Description,
                    Color = model.Color,
                    DateMisplaced = model.DateMisplaced,
                    ExactArea = model.ExactArea,
                    WhereItemWasLost = model.WhereItemWasLost,
                    LocalGovernmentId = model.LocalGovernmentId,
                    Image = image,
                };

                repository.Create(itemm);
                repository.Save();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.StateList = await stateRepository.GetAllStates();
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await repository.GetLostItemById(id);

            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lostItem = await repository.GetLostItemById(id);
            ViewBag.LGA = await lgaRepository.GetAllLGAByStateId(lostItem.LocalGovernment.StateId);

            if (lostItem == null)
            {
                return NotFound();
            }
            return View(lostItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LostItem model, IFormFile file = null)
        {
            if (id != model.Id)
            { return NotFound(); }

            var lostItem = await repository.GetLostItemById(id);
            Image image = null;
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (!utility.IsSizeAllowed(file))
                    {
                        ModelState.AddModelError("Photo", "Your file is too large, maximum allowed size is: 5MB");
                        return View(file);
                    }

                    if (!utility.IsImageExtensionAllowed(file))
                    {
                        ModelState.AddModelError("Photo", "Please only file of type:.jpg, .jpeg, .gif, .png, .bmp  are allowed");
                        return View(file);
                    }
                    var photoPath = utility.SaveImageToFolder(file);
                    image = new Image { ImagePath = photoPath };
                    lostItem.Image = image;
                }
                try
                {
                    lostItem.NameOfLostItem = model.NameOfLostItem;
                    lostItem.Description = model.Description;
                    lostItem.ItemCategory = model.ItemCategory;
                    lostItem.ExactArea = model.ExactArea;
                    lostItem.WhereItemWasLost = model.WhereItemWasLost;
                    lostItem.Color = model.Color;
                    lostItem.DateMisplaced = model.DateMisplaced;
                    lostItem.LocalGovernmentId = model.LocalGovernmentId;
                    lostItem.ModifiedOn = DateTime.UtcNow;
                    repository.Update(lostItem);
                    repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostItemExists(lostItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: lostItems/Delete/5
        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            { return NotFound(); }

            var lostItem = await repository.GetLostItemById(id);
            repository.Delete(lostItem);
            repository.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Deletes")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var lostItem = await repository.GetLostItemById(id);
            lostItem.IsDeleted = true;
            lostItem.DeletedOn = DateTime.Now;

            foreach (var claim in lostItem.LostItemClaims)
            {
                claim.Status = ClaimStatus.Invalid;
            }
            repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool LostItemExists(int id)
        {
            return repository.LostItemExists(id);
        }

        public async Task<IActionResult> Claims(int? id)
        {
            var claims = await claimRepository.GetLostItemClaimsbyLostItemId(id);
            return View(claims);
        }

        [HttpPost]
        public async Task<IActionResult> Validate(int? id, string status)
        {
            if (id == null)
                return NotFound();
            var claim = await claimRepository.GetLostItemClaimById(id);
            try
            {
                if (status == "Valid")
                { claim.Status = ClaimStatus.Valid; }
                if (status == "InValid")
                { claim.Status = ClaimStatus.Invalid; }
                claim.ValidatedOn = DateTime.Now;

                // string to = claim.ApplicationUser.Email;
                var link = Url.Action("Index", "ClaimManagement", new { area = "Admin" });
                var message = new Dictionary<string, string>
                {
                   {"ITEM",$"Lost Item" },

                   {"EmailLink",$"{link}" }
                };

                var usersInAdmin = await userManager.GetUsersInRoleAsync("Admin");
                List<string> emails = new List<string>();
                foreach (var user in usersInAdmin)
                {
                    emails.Add(user.Email);
                }

                await emailNotifier.SendManyEmailAsync(emails, "Validation", message, "AlertAdmin");

                claimRepository.Save();
                return RedirectToAction("Claims", new { Id = claim.LostItemId });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<IActionResult> Claims(int id)
        //{
        //    var claims = await claimRepository.(id);
        //    return View(claims);
        //}
    }
}