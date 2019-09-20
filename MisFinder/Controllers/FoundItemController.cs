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

namespace MisFinder.Controllers
{
    [Authorize]
    public class FoundItemController : Controller
    {
        private readonly MisFinderDbContext context;
        private readonly IFoundItemRepository repository;
        private readonly IStateRepository staterepository;
        private readonly UserManager<ApplicationUser> userManager;

        public FoundItemController(MisFinderDbContext context,
            IFoundItemRepository repository,IStateRepository staterepository, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.repository = repository;
            this.staterepository = staterepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            var foundItems= repository.GetFoundItemsByUser(user);
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
            var foundItem = repository.GetFoundItemById(id);
            return View(foundItem);
        }
        [HttpGet]
        public IActionResult Create()
        {

           return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FoundItemViewModel item)
        {var user= await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            { user.PhoneNumber = item.PhoneNumber;
                FoundItem itemm = new FoundItem
                { 
                    FoundItemUser = user,
                    NameOfFoundItem = item.Name,
                    Location = item.State,
                    Description = item.Description,
                    Colour = item.Colour,
                    DateFound = item.DateFound
                    
                };
                repository.Create(itemm);
               repository.Save();
                ViewBag.StateList = staterepository.GetAllStates();
                return RedirectToAction(nameof(Index));
            }
            return View(item);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItem = repository.GetFoundItemById(id);

            if (foundItem == null)
            {
                return NotFound();
            }
            return View(foundItem);
        }
        [HttpPost]
        public IActionResult Edit(int id, FoundItem foundItem)
        {
            if (id != foundItem.Id)
            { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(foundItem);
                    repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoundItemExists(foundItem.Id))
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
            return View(foundItem);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundItem = await context.FoundItems.ToListAsync();
                

            if (foundItem == null)
            {
                return NotFound();
            }

            //            var twitter= new MiTwitter()

            return View(foundItem);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var FoundItem = await context.FoundItems
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (FoundItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(FoundItem);
        //}

        // POST: FoundItems/Delete/5
        [HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foundItem = await context.FoundItems.SingleOrDefaultAsync(m => m.Id == id);
            context.FoundItems.Remove(foundItem);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FoundItemExists(int id)
        {
            return context.FoundItems.Any(e => e.Id == id);
        }


    }
}