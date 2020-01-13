using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Utility;
using Microsoft.AspNetCore.Http;
using MisFinder.Data.Notification.Email;
using System.ComponentModel.DataAnnotations;

namespace MisFinder.Areas.User.Controllers

{
    [Area("User")]
    public class FoundItemController : Controller
    {
        private readonly MisFinderDbContext context;
        private readonly IUtility utility;
        private readonly IFoundItemRepository repository;
        private readonly IStateRepository staterepository;
        private readonly ILocalGovernmentRepository lgaRepository;
        private readonly IFoundItemClaimRepository claimRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailNotifier emailNotifier;

        public FoundItemController(MisFinderDbContext context, IUtility utility,
            IFoundItemRepository repository, IStateRepository staterepository,
            ILocalGovernmentRepository lgaRepository,
            IFoundItemClaimRepository claimRepository,
            UserManager<ApplicationUser> userManager, IEmailNotifier emailNotifier)
        {
            this.context = context;
            this.utility = utility;
            this.repository = repository;
            this.staterepository = staterepository;
            this.lgaRepository = lgaRepository;
            this.claimRepository = claimRepository;
            this.userManager = userManager;
            this.emailNotifier = emailNotifier;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            var foundItems = await repository.GetFoundItemsByUser(user);
            return View(foundItems);
        }

        [AllowAnonymous]
        public IActionResult GetFoundItems()
        {
            var foundItems = repository.GetAllFoundItems();
            return View(foundItems);
        }

        public async Task<IActionResult> GetFoundItemById(int? id)
        {
            if (id == null)
                return NotFound();
            var foundItem = await repository.GetFoundItemById(id);
            return View(foundItem);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.StateList = await staterepository.GetAllStates();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoundItemViewModel item)
        {
            ViewBag.StateList = await staterepository.GetAllStates();

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                Image image = null;
                if (item.Photo != null)
                {
                    if (!utility.IsSizeAllowed(item.Photo))
                    {
                        ModelState.AddModelError("Photo", "Your file is too large, maximum allowed size is: 5MB");
                        return View(item);
                    }

                    if (!utility.IsImageExtensionAllowed(item.Photo))
                    {
                        ModelState.AddModelError("Photo", "Please only file of type:.jpg, .jpeg, .gif, .png, .bmp  are allowed");
                        return View(item);
                    }
                    var photopath = utility.SaveImageToFolder(item.Photo);
                    image = new Image { ImagePath = photopath };
                }
                user.PhoneNumber = item.PhoneNumber;
                FoundItem itemm = new FoundItem
                {
                    FoundItemUser = user,
                    NameOfFoundItem = item.Name,
                    //State = item.State,
                    Description = item.Description,
                    Colour = item.Colour,
                    DateFound = item.DateFound,
                    WhereItemWasFound = item.WhereItemWasFound,
                    ExactArea = item.ExactArea,
                    Image = image,
                    LocalGovernmentId = item.LocalGovernmentId,
                };
                repository.Create(itemm);
                repository.Save();
                ViewBag.StateList = staterepository.GetAllStates();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItem = await repository.GetFoundItemById(id);
            ViewBag.LGA = await lgaRepository.GetAllLGAByStateId(foundItem.LocalGovernment.StateId);
            if (foundItem == null)
            {
                return NotFound();
            }
            return View(foundItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FoundItem model, IFormFile file = null)
        {
            if (id != model.Id)
            { return NotFound(); }

            var foundItem = await repository.GetFoundItemById(id);
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
                    foundItem.Image = image;
                }
                try
                {
                    foundItem.NameOfFoundItem = model.NameOfFoundItem;
                    foundItem.Description = model.Description;
                    foundItem.ItemCategory = model.ItemCategory;
                    foundItem.ExactArea = model.ExactArea;
                    foundItem.WhereItemWasFound = model.WhereItemWasFound;
                    foundItem.Colour = model.Colour;
                    foundItem.DateFound = model.DateFound;
                    foundItem.LocalGovernmentId = model.LocalGovernmentId;
                    foundItem.ModifiedOn = DateTime.UtcNow;
                    foundItem.IsEditedCount += 1;

                    repository.Update(foundItem);
                    repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoundItemExists(model.Id))
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItem = await repository.GetFoundItemById(id);

            if (foundItem == null)
            {
                return NotFound();
            }

            //            var twitter= new MiTwitter()

            return View(foundItem);
        }

        // POST: FoundItems/Delete/5
        [HttpGet, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foundItem = await repository.GetFoundItemById(id);
            repository.Delete(foundItem);
            repository.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Deletes")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var foundItem = await repository.GetFoundItemById(id);
            foundItem.IsDeleted = true;
            foundItem.DeletedOn = DateTime.UtcNow;
            repository.Save();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Claims(int? id)
        {
            if (id == null)
                return NotFound();
            var claims = await claimRepository.GetFoundItemClaimsbyFoundItemId(id);
            return View(claims);
        }

        private bool FoundItemExists(int id)
        {
            return repository.FoundItemExists(id);
        }

        public async Task<IActionResult> Validate(int? id, string status)
        {
            if (id == null)
                return NotFound();
            var claim = await claimRepository.GetFoundItemClaimById(id);
            if (claim == null)
                return NotFound();
            //if(status==Pending)
            if (status == "Valid")
            { claim.Status = ClaimStatus.Valid; }
            if (status == "InValid")
            { claim.Status = ClaimStatus.Invalid; }
            claim.ValidatedOn = DateTime.Now;

            var link = Url.Action("ValidatedFoundItemsClaim", "ClaimManagement", new { area = "Admin" }, Request.Scheme);
            System.IO.File.WriteAllText("validation.txt", link);
            //var message = new Dictionary<string, string>
            //    {
            //       {"ITEM",$"Found Item" },

            //       {"EmailLink",$"{link}" }
            //    };

            //var usersInAdmin = await userManager.GetUsersInRoleAsync("Admin");
            //List<string> emails = new List<string>();
            //foreach (var user in usersInAdmin)
            //{
            //    emails.Add(user.Email);
            //}

            //await emailNotifier.SendManyEmailAsync(emails, "Validation", message, "AlertAdmin");

            claimRepository.Save();

            return RedirectToAction("Claims", new { Id = claim.FoundItemId });
        }
    }
}