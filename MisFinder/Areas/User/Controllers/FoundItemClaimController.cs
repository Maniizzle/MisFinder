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
    public class FoundItemClaimController : Controller
    {
        private readonly IFoundItemClaimRepository claimRepository;
        private readonly IEmailNotifier emailNotifier;
        private readonly IFoundItemRepository foundItemRepository;
        private readonly IUtility utility;
        private readonly UserManager<ApplicationUser> userManager;

        public FoundItemClaimController(IFoundItemClaimRepository claimRepository,
            IEmailNotifier emailNotifier, IFoundItemRepository foundItemRepository, IUtility utility,
            UserManager<ApplicationUser> userManager)
        {
            this.claimRepository = claimRepository;
            this.emailNotifier = emailNotifier;
            this.foundItemRepository = foundItemRepository;
            this.utility = utility;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            var claims = await claimRepository.GetFoundItemClaimsByUser(user);
            return View(claims);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var claim = new FoundItemClaimViewModel { FoundItemId = id };

            //ViewBag.StateList = await claimRepository.GetAllStates();

            return View(claim);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoundItemClaimViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var foundItem = await foundItemRepository.GetFoundItemById(model.FoundItemId);

            if (user == null || user == foundItem.FoundItemUser)
            {
                ModelState.AddModelError("", "You Cant Claim an Item you Logged");
                return View();
            }

            if (ModelState.IsValid)
            {
                foreach (var clam in foundItem.FoundItemClaims)
                {
                    if (user == clam.ApplicationUser)
                    {
                        ModelState.AddModelError("", "You Cant claim an Item twice");
                        return View();
                    }
                }
                Image image = null;
                if (model.Image != null)
                {
                    if (!utility.IsSizeAllowed(model.Image))
                    {
                        ModelState.AddModelError("Photo", "Your file is too large, maximum allowed size is: 5MB");
                        return View(model);
                    }

                    if (!utility.IsImageExtensionAllowed(model.Image))
                    {
                        ModelState.AddModelError("Photo", "Please only file of type:.jpg, .jpeg, .gif, .png, .bmp  are allowed");
                        return View(model);
                    }
                    var photoPath = utility.SaveImageToFolder(model.Image);
                    image = new Image { ImagePath = photoPath };
                }

                user.PhoneNumber = user.PhoneNumber ?? model.PhoneNumber;
                var claim = new FoundItemClaim
                {
                    ApplicationUser = user,
                    FoundItemId = model.FoundItemId,
                    Description = model.Description,
                    DateLost = model.DateLost,
                    WhereItemWasLost = model.WhereItemWasLost,
                    Image = image,
                };
                claimRepository.Create(claim);
                claimRepository.Save();
                var ConfirmEmail = Url.Action("Claims", "FoundItem",
                     new { area = User, id = foundItem.Id }, Request.Scheme);
                var message = new Dictionary<string, string>
                    {
                        {"UName",$"{foundItem.FoundItemUser.FirstName}" },
                        { "FName",$"{user.FirstName}" },
                        {"ClaimLink", $"{ConfirmEmail}" }
                    };

                await emailNotifier.SendEmailAsync(foundItem.FoundItemUser.Email, "New Claim", message, "foundItemClaim");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItem = claimRepository.GetFoundItemClaimById(id);

            if (foundItem == null)
            {
                return NotFound();
            }
            return View(foundItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FoundItemClaim model, IFormFile file = null)
        {
            if (id != model.Id)
            { return NotFound(); }

            var foundItemClaim = await claimRepository.GetFoundItemClaimById(id);
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
                    foundItemClaim.Image = image;
                }
                try
                {
                    foundItemClaim.DateLost = model.DateLost;
                    foundItemClaim.Description = model.Description;
                    foundItemClaim.WhereItemWasLost = model.WhereItemWasLost;
                    foundItemClaim.Color = model.Color;
                    foundItemClaim.ModifiedOn = DateTime.UtcNow;
                    foundItemClaim.IsEditedCount += 1;

                    claimRepository.Update(foundItemClaim);
                    claimRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoundItemClaimExists(foundItemClaim.Id))
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
            return View(foundItemClaim);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItemClaim = await claimRepository.GetFoundItemClaimById(id);

            if (foundItemClaim == null)
            {
                return NotFound();
            }

            //            var twitter= new MiTwitter()

            return View(foundItemClaim);
        }

        // POST: FoundItems/Delete/5
        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foundItem = await claimRepository.GetFoundItemClaimById(id);
            claimRepository.Delete(foundItem);
            claimRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Deletes")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var foundItemClaim = await claimRepository.GetFoundItemClaimById(id);
            foundItemClaim.IsDeleted = true;
            foundItemClaim.DeletedOn = DateTime.UtcNow;
            claimRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool FoundItemClaimExists(int id)
        {
            return claimRepository.FoundItemClaimsExists(id);
        }
    }
}