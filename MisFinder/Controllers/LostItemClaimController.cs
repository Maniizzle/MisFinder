using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;

namespace MisFinder.Controllers
{
    public class LostItemClaimController : Controller
    {
        private readonly ILostItemClaimRepository claimRepository;

        public LostItemClaimController(ILostItemClaimRepository claimRepository)
        {
            this.claimRepository = claimRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<LostItemClaim> claims= await claimRepository.GetAllLostItemClaims();
            return View(claims);
        }
    }
}