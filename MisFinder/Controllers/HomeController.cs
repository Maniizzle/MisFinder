using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Data.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MisFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoundItemRepository repository;
        private readonly ILocalGovernmentRepository localGovernmentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILostItemRepository lostItemRepository;
        private readonly IStateRepository stateRepository;

        public HomeController(IFoundItemRepository repository,ILocalGovernmentRepository localGovernmentRepository,
            UserManager<ApplicationUser> userManager, ILostItemRepository lostItemRepository,IStateRepository stateRepository)
        {
            this.repository = repository;
            this.localGovernmentRepository = localGovernmentRepository;
            this.userManager = userManager;
            this.lostItemRepository = lostItemRepository;
            this.stateRepository = stateRepository;
        }
        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            TempData["States"]
             = await stateRepository.GetAllStates();

            return View();
        }

        //public IActionResult Search(string name)
        //{
        //  var result=  context.LostItems.
        //        Where(c=>(c.NameOfLostItem.ToLower().Contains(name.ToLower())||c.Description.ToLower().Contains(name.ToLower()) ));
        //    return View(result); 
        //}
        [AllowAnonymous]

        public async Task<IActionResult> SearchFoundItem(SearchViewModel model)
        {
            if (ModelState.IsValid) {
           var result=  repository.SearchFoundItem(model);
                return View(await result.AsNoTracking().ToListAsync());
            }
            return View("Index",model);
        }
        [AllowAnonymous]

        public async Task<IActionResult> SearchLostItem(SearchViewModel model)
        {
            if (model == null)
                return NotFound();
            if (ModelState.IsValid)
            {

                var result =  lostItemRepository.SearchLostItem(model);
                return View(await result.AsNoTracking().ToListAsync());
            }
          // TempData["States"]
          //=  await stateRepository.GetAllStates();

            return View("Index", model);

        }
        [AllowAnonymous]

        public async Task<IEnumerable<LocalGovernment>> LocalGovernments(int id)
        {
            return await localGovernmentRepository.GetAllLGAByStateId(id);
        }

        ////
        ///[HttpGet]
        //public IActionResult FoundItems()
        //{

        //    IEnumerable<FoundItem> foundItems = context.FoundItems.ToList();
        //    return View(foundItems);
        //}
        //[HttpGet]
        //public IActionResult LostItems()
        //{
        //    IEnumerable<LostItem> lostItems = context.LostItems.ToList();
        //    return View(lostItems);

        //}
    }
}