using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Domain.Models;

namespace MisFinder.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        public DashBoardController(UserManager<ApplicationUser> userManager)
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}