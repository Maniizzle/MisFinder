using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Data.Data.Context;

namespace MisFinder.Controllers
{
    [Authorize]
    public class LostItemController : Controller
    {
        private readonly MisFinderDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public LostItemController(MisFinderDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();
            IEnumerable<LostItem> lostitem = context.LostItems.Where(c=>c.LostItemUser==user).ToList();
            return View(lostitem);
        }
        [AllowAnonymous]
        public IActionResult GetLostItems()
        {
            IEnumerable<LostItem> lostItems = context.LostItems.Include(c => c.FoundLostItems);
            return View(lostItems);
        }
        public async Task<IActionResult> GetLostItemById(int? id)
        {
            if (id == null)
                return NotFound();
            var lostItem = await context.LostItems.SingleOrDefaultAsync(c => c.Id == id);
            return View(lostItem);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(LostItemViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                    user.PhoneNumber = model.PhoneNumber;
                LostItem itemm = new LostItem
                {
                    LostItemUser = user,
                    NameOfLostItem = model.NameOfLostItem,
                    Location = model.Location,
                    Description = model.Description,
                    Color = model.Color,
                    DateMisplaced = model.DateMisplaced,
                    DateCreated = DateTime.Now
                         
                    };

                    context.Add(itemm);
                    await context.SaveChangesAsync();
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

            var quote = await context.LostItems
                .Include(q => q.FoundLostItems)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem =context.LostItems.SingleOrDefault(m => m.Id == id);
                                                 
            if (lostItem == null)
            {
                return NotFound();
            }
            return View(lostItem);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, LostItem lostItem)
        {
            if (id != lostItem.Id)
            { return NotFound();}

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(lostItem);
                    await context.SaveChangesAsync();
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
            return View(lostItem);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lostItem = await context.LostItems.SingleOrDefaultAsync(m => m.Id == id);
            context.LostItems.Remove(lostItem);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool LostItemExists(int id)
        {
            return context.LostItems.Any(e => e.Id == id);
        }

       
    }
}