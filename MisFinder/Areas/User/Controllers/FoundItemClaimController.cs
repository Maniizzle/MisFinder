using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Utility;

namespace MisFinder.Areas.User.Controllers
{
    [Area("User")]
    
    public class FoundItemClaimController : Controller
    {
        private readonly IFoundItemClaimRepository claimRepository;
        private readonly IUtility utility;
        private readonly UserManager<ApplicationUser> userManager;

        public FoundItemClaimController(IFoundItemClaimRepository claimRepository,IUtility utility,
            UserManager<ApplicationUser> userManager)
        {
            
            this.claimRepository = claimRepository;
            this.utility = utility;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
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

            if (ModelState.IsValid)
            {
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
                    image = new Image { ImagePath =photoPath  };
                }
                
                user.PhoneNumber = user.PhoneNumber ?? model.PhoneNumber;
                var claim = new FoundItemClaim
                {
                    ApplicationUser = user,
                    FoundItemId = model.FoundItemId,
                    Description = model.Description,
                     DateLost=model.DateLost,
                      WhereItemWasLost=model.WhereItemWasLost,
                      Image= image,

                };
                claimRepository.Create(claim);
                claimRepository.Save();

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
                        foundItemClaim.Color   = model.Color;
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