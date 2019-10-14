using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                var claim = new FoundItemClaim
                {
                    ApplicationUser = user,
                    FoundItemId = model.FoundItemId,
                    Description = model.Description,
                     DateLost=model.DateLost,
                      ExactAreaLost=model.ExactAreaLost,
                    CreatedAt = DateTime.Now,

                };
                claimRepository.Create(claim);
                claimRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(model);


        }

    }
}