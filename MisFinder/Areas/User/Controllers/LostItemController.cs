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
using MisFinder.Data.Persistence.IRepositories;

namespace MisFinder.Areas.User.Controllers

{
   
    [Area("User")]
    public class LostItemController : Controller
    {
        private readonly IStateRepository stateRepository;
        private readonly ILocalGovernmentRepository lgaRepository;
        private readonly ILostItemRepository repository;
        private readonly ILostItemClaimRepository claimRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public LostItemController(IStateRepository stateRepository,ILocalGovernmentRepository lgaRepository,
            ILostItemRepository repository, ILostItemClaimRepository claimRepository,UserManager<ApplicationUser> userManager)
        {
            this.stateRepository = stateRepository;
            this.lgaRepository = lgaRepository;
            this.repository = repository;
            this.claimRepository = claimRepository;
           // this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return NotFound();

            IEnumerable<LostItem> lostitem = await repository.GetLostItemsByUser(user);
            return View(lostitem);
        }
        [AllowAnonymous]
        
        public async Task<IActionResult> GetLostItems()
        {
            IEnumerable<LostItem> lostItems = await repository.GetAllLostItems(); 
            return View(lostItems);
        }
        public async Task<IActionResult> LostItemById(int? id)
        {
            if (id == null)
                return NotFound();
            var lostItem = await repository.GetLostItemsById(id); 
            return View(lostItem);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.StateList = await stateRepository.GetAllStates();
            
            return View();
        } 
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
                    //StateId = model.StateId,
                    Description = model.Description,
                    Color = model.Color,
                    DateMisplaced = model.DateMisplaced,
                    ExactArea= model.ExactArea,
                    WhereItemWasLost    =model.WhereItemWasLost,
                    LocalGovernmentId =model.LocalGovernmentId,
                    CreatedAt = DateTime.Now,
                         
                    };

                repository.Create(itemm);
                repository.Save();
                
                return RedirectToAction(nameof(Index));
            }
            ViewBag.StateList = await stateRepository.GetAllStates();
            return View(model);

            
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await repository.GetLostItemById(id); 
                
            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem =await repository.GetLostItemById(id);
                                                 
            if (lostItem == null)
            {
                return NotFound();
            }
            return View(lostItem);
        }
        [HttpPost]
        public  IActionResult Edit(int id, LostItem lostItem)
        {
            if (id != lostItem.Id)
            { return NotFound();}

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(lostItem);
                    repository.Save();
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
        [HttpGet,ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var lostItem = await repository.GetLostItemById(id);
             repository.Delete(lostItem);
            repository.Save();
            return RedirectToAction(nameof(Index));
        }
       

        private bool LostItemExists(int id)
        {
            return repository.LostItemExists(id);
        }

       public async Task<IActionResult> LostItemClaims(int? id)
        {
          var claims=  await claimRepository.GetLostItemClaimsById(id);
            return View(claims);
        }
    }
}