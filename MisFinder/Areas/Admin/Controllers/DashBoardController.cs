using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ILostItemRepository lostrepository;
        private readonly IFoundItemRepository foundrepository;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(ILostItemRepository lostrepository,IFoundItemRepository foundrepository,
            UserManager<ApplicationUser> userManager)
        {
            this.lostrepository = lostrepository;
            this.foundrepository = foundrepository;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LostItems()
        {
            var lostitems= lostrepository.GetAllLostItems();
            return View(lostitems);
        }
    }
}