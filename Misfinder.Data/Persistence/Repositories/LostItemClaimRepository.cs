using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.Repositories
{
   public class LostItemClaimRepository : ILostItemClaimRepository
    {
        private readonly MisFinderDbContext context;

        public LostItemClaimRepository(MisFinderDbContext context)
        {
            this.context = context;
        }
        public void Create(LostItemClaim entity)
        {
            context.LostItemClaims.Add(entity);

        }

        public void Delete(LostItemClaim entity)
        {
            context.Remove(entity);

        }

        public  async Task Delete(int id)
        {
            var lostItemClaims = await context.LostItemClaims.SingleOrDefaultAsync(m => m.Id == id);
            context.LostItemClaims.Remove(lostItemClaims);
        
        }
        public async void Save()
        {
            await context.SaveChangesAsync();

        }

        public void Update(LostItemClaim entity)
        {
            context.Entry(entity).State = EntityState.Modified;

        }
        public async Task<IEnumerable<LostItemClaim>> GetAllLostItemClaims()
        {
            IEnumerable<LostItemClaim> lostItems = await context.LostItemClaims.ToListAsync();
            return lostItems;
           
        }

        public Task<LostItemClaim> GetLostItemClaimById(int? id)
        {
          
            var lostItem = context.LostItemClaims.FirstOrDefaultAsync(c => c.Id == id);
            return lostItem;
        }

        public async Task<IEnumerable<LostItemClaim>> GetLostItemClaimsById(int? id)
        {
            IEnumerable<LostItemClaim> lostItemClaims = await context.LostItemClaims.Where(c => c.Id == id).ToListAsync();
            return lostItemClaims;
        }

        public async Task<IEnumerable<LostItemClaim>> GetLostItemClaimsByUser(ApplicationUser user)
        {
            IEnumerable<LostItemClaim> lostItem = await context.LostItemClaims.Where(c => c.ApplicationUser == user).ToListAsync();
            return lostItem;
        }
        public bool LostItemClaimsExists(int id)
        {
            return context.LostItemClaims.Any(e => e.Id == id);
        }

    }
}
