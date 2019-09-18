using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MisFinder.Controllers
{
    public class ApplicationUserController : Controller
    {
        public IActionResult Profile()
        {
           
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        
    //    public async Task<IActionResult> GetAll(int? id)
    //    {
    //        if (id == null)
    //            return NotFound();
    //        var FoundItem = await context.FoundItems.SingleOrDefaultAsync(c => c.Id == id);
    //        return View(FoundItem);
    //    }
    //    public IActionResult Create() => View();

    //    public async Task<IActionResult> Create(FoundItem item)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            context.Add(item);
    //            await context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(item);

    //    }
    //    public IActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var FoundItem = context.FoundItems.SingleOrDefault(m => m.Id == id);

    //        if (FoundItem == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(FoundItem);
    //    }

    //    public async Task<IActionResult> Edit(int id, FoundItem foundItem)
    //    {
    //        if (id != foundItem.Id)
    //        { return NotFound(); }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                context.Update(foundItem);
    //                await context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!FoundItemExists(foundItem.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(foundItem);
    //    }

    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var foundItem = await context.FoundItems.ToListAsync();


    //        if (foundItem == null)
    //        {
    //            return NotFound();
    //        }

    //        //            var twitter= new MiTwitter()

    //        return View(foundItem);
    //    }

    //    //public async Task<IActionResult> Delete(int? id)
    //    //{
    //    //    if (id == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    var FoundItem = await context.FoundItems
    //    //        .SingleOrDefaultAsync(m => m.Id == id);
    //    //    if (FoundItem == null)
    //    //    {
    //    //        return NotFound();
    //    //    }

    //    //    return View(FoundItem);
    //    //}

    //    // POST: FoundItems/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var foundItem = await context.FoundItems.SingleOrDefaultAsync(m => m.Id == id);
    //        context.FoundItems.Remove(foundItem);
    //        await context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }


    //    private bool FoundItemExists(int id)
    //    {
    //        return context.FoundItems.Any(e => e.Id == id);
    //    }

    }
}