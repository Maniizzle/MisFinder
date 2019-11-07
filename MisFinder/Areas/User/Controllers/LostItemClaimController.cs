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
    public class LostItemClaimController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailNotifier emailNotifier;
        private readonly IUtility utility;
        private readonly ILostItemRepository lostItemRepository;
        private readonly ILostItemClaimRepository claimRepository;

        public LostItemClaimController(UserManager<ApplicationUser> userManager,
            IEmailNotifier emailNotifier, IUtility utility, ILostItemRepository lostItemRepository, ILostItemClaimRepository claimRepository)
        {
            this.userManager = userManager;
            this.emailNotifier = emailNotifier;
            this.utility = utility;
            this.lostItemRepository = lostItemRepository;
            this.claimRepository = claimRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            var claims = await claimRepository.GetLostItemClaimsByUser(user);
            return View(claims);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var claim = new LostItemClaimViewModel { LostItemId = id };
            //ViewBag.StateList = await claimRepository.GetAllStates();

            return View(claim);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LostItemClaimViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var lostItem = await lostItemRepository.GetLostItemById(model.LostItemId);
            if (user == null || user == lostItem.LostItemUser)
            {
                ModelState.AddModelError("", "You Cant Claim an Item you Logged");
                return View();
            }
            if (ModelState.IsValid)
            {
                //Check if user has claimed Item before
                foreach (var clam in lostItem.LostItemClaims)
                {
                    if (user.Id == clam.ApplicationUserId)
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
                    var photopath = utility.SaveImageToFolder(model.Image);
                    image = new Image { ImagePath = photopath };
                }
                user.PhoneNumber = model.PhoneNumber;

                user.PhoneNumber = user.PhoneNumber ?? model.PhoneNumber;
                LostItemClaim claim = new LostItemClaim
                {
                    ApplicationUser = user,
                    LostItemId = model.LostItemId,
                    Description = model.Description,
                    DateFound = model.DateFound,
                    WhereItemWasFound = model.WhereItemWasFound,
                    Image = image
                };
                claimRepository.Create(claim);
                //  System.IO.File.WriteAllText("resetlink.txt", ConfirmEmail);
                //send mail
                var ConfirmClaim = Url.Action("Claims", "LostItem",
                      new { area = "User", id = lostItem.Id }, Request.Scheme);

                var message = new Dictionary<string, string>
                    {
                        {"UName",$"{lostItem.LostItemUser.FirstName}" },
                        { "FName",$"{user.FirstName}" },
                        {"ClaimLink", $"{ConfirmClaim}" }
                    };

                await emailNotifier.SendEmailAsync(lostItem.LostItemUser.Email, "New Claim", message, "LostItemClaim");
                claimRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //[AcceptVerbs("Get", "Post")]
        //public async Task<IActionResult> SameUser([Bind(Prefix = "id")] int id)
        //{
        //    var user = await userManager.GetUserAsync(HttpContext.User);
        //    var lostItem = await lostItemRepository.GetLostItemById(id);
        //    if (user == null || user != lostItem.LostItemUser)
        //        return RedirectToAction("Create", "LostItemClaim", new { Id = id });
        //    //.FindByEmailAsync();
        //    // return Json("You cant Claim to have Found what you declared you lost");
        //    return View();
        //}

        public async Task<IActionResult> MakePayment()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            CyberPay cyberPay = new CyberPay
            {
                Amount = 1500 * 100,
                Description = "Delivery Payment ",
                MerchantRef = Guid.NewGuid().ToString(),
                ReturnUrl = @"https://localhost:5001/User/LostItemClaim/Index",
                CustomerEmail = user.Email,
                CustomerMobile = user.PhoneNumber ?? "",
                CustomerName = $"{user.LastName} {user.FirstName}"
            };
            var res = await Processing.MakePaymentAsync(cyberPay);
            if (res.Succeeded)
            {
                return Redirect(res.Data.RedirectUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItemClaim = await claimRepository.GetLostItemClaimById(id);

            if (lostItemClaim == null)
            {
                return NotFound();
            }

            return View(lostItemClaim);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lostItem = await claimRepository.GetLostItemClaimById(id);
            //ViewBag.LGA = await lgaRepository.GetAllLGAByStateId(lostItem.LocalGovernment.StateId);

            if (lostItem == null)
            {
                return NotFound();
            }
            return View(lostItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LostItemClaim model, IFormFile file = null)
        {
            if (id != model.Id)
            { return NotFound(); }

            var lostItemClaim = await claimRepository.GetLostItemClaimById(id);
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
                    lostItemClaim.Image = image;
                }
                try
                {
                    lostItemClaim.ItemCategory = model.ItemCategory;
                    lostItemClaim.Description = model.Description;
                    lostItemClaim.DateFound = model.DateFound;
                    claimRepository.Update(lostItemClaim);
                    claimRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!claimRepository.LostItemClaimsExists(lostItemClaim.Id))
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

        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var lostItem = await claimRepository.GetLostItemClaimById(id);
            claimRepository.Delete(lostItem);
            claimRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Deletes")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var lostItem = await claimRepository.GetLostItemClaimById(id);
            lostItem.IsDeleted = true;
            lostItem.DeletedOn = DateTime.UtcNow;
            claimRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        //private bool LostItemExists(int id)
        //{
        //    return claimRepository.LostItemClaimExists(id);
        //}

        public async Task<IActionResult> LostItemClaims(int? id)
        {
            var claims = await claimRepository.GetLostItemClaimsById(id);
            return View(claims);
        }
    }
}