using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Utility;

namespace MisFinder.Areas.User.Controllers

{

    [Area("User")]
    [Authorize(Roles = "User")]
    public class LostItemClaimController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUtility utility;
        private readonly ILostItemRepository lostItemRepository;
        private readonly ILostItemClaimRepository claimRepository;

        public LostItemClaimController(UserManager<ApplicationUser> userManager, IUtility utility, ILostItemRepository lostItemRepository, ILostItemClaimRepository claimRepository)
        {
            this.userManager = userManager;
            this.utility = utility;
            this.lostItemRepository = lostItemRepository;
            this.claimRepository = claimRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<LostItemClaim> claims = await claimRepository.GetAllLostItemClaims();
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

            if (ModelState.IsValid)
            {
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
                }
                var photopath = utility.SaveImageToFolder(model.Image);
                user.PhoneNumber = model.PhoneNumber;

                user.PhoneNumber = user.PhoneNumber ?? model.PhoneNumber;
                LostItemClaim claim = new LostItemClaim
                {
                    ApplicationUser = user,
                    LostItemId = model.LostItemId,
                    Description = model.Description,

                };
                claimRepository.Create(claim);
                claimRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(model);


        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> SameUser([Bind(Prefix = "id")] int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var lostItem = await lostItemRepository.GetLostItemById(id);
            if (user == null || user != lostItem.LostItemUser)
                return RedirectToAction("Create", "LostItemClaim", new { Id = id });
            //.FindByEmailAsync();
            return Json("You cant Claim to have Found what you declared you lost");
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
        public IActionResult Edit(int id, LostItemClaim lostItemClaim, IFormFile file = null)
        {
            if (id != lostItemClaim.Id)
            { return NotFound(); }
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
                    claimRepository.Update(lostItemClaim);
                    claimRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!LostItemExists(lostItemClaim.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lostItemClaim);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lostItem = await context.LostItems
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (lostItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lostItem);
        //}

        // POST: lostItems/Delete/5
        [HttpGet, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var lostItem = await claimRepository.GetLostItemClaimById(id);
            claimRepository.Delete(lostItem);
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
