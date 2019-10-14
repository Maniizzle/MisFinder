using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Utility;

namespace MisFinder.Areas.User.Controllers

{
    
    [Area("User")]
    public class LostItemClaimController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUtility utility;
        private readonly ILostItemRepository lostItemRepository;
        private readonly ILostItemClaimRepository claimRepository;

        public LostItemClaimController(UserManager<ApplicationUser> userManager,IUtility utility, ILostItemRepository lostItemRepository,  ILostItemClaimRepository claimRepository)
        {
            this.userManager = userManager;
            this.utility = utility;
            this.lostItemRepository = lostItemRepository;
            this.claimRepository = claimRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<LostItemClaim> claims= await claimRepository.GetAllLostItemClaims();
            return View(claims);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {

            var claim = new LostItemClaimViewModel {LostItemId=id };

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
                    CreatedAt = DateTime.Now,

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
            if (user == null  || user != lostItem.LostItemUser)
                return RedirectToAction("Create", "LostItemClaim", new { Id = id });
            //.FindByEmailAsync();
            return Json("You cant Claim to have Found what you declared you lost");
        }
    }
}