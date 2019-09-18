using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Data.Data.Context;

namespace MisFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly MisFinderDbContext context;


        public HomeController( MisFinderDbContext context)
        {
            this.context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Search(string name)
        {
          var result=  context.LostItems.
                Where(c=>(c.NameOfLostItem.ToLower().Contains(name.ToLower())||c.Description.ToLower().Contains(name.ToLower()) ));
            return View(result); 
        }
        public IActionResult Search2(string name)
        {
            var result = context.FoundItems.
                  Where(c => (c.Name.ToLower().Contains(name.ToLower()) || c.Description.ToLower().Contains(name.ToLower())));
            return View(result);
        }
        [HttpGet]
        public IActionResult FoundItems()
        {

            IEnumerable<FoundItem> foundItems = context.FoundItems.ToList();
            return View(foundItems);
        }
        [HttpGet]
        public IActionResult LostItems()
        {
            IEnumerable<LostItem> lostItems = context.LostItems.ToList();
            return View(lostItems);

        }
    }
}