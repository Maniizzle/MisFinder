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
    public class FoundItemClaimRepository : IFoundItemClaimRepository
    {
        private readonly MisFinderDbContext context;

        public FoundItemClaimRepository(MisFinderDbContext context)
        {
            this.context = context;
        }
        public void Create(FoundItemClaim entity)
        {
            context.FoundItemClaims.Add(entity);

        }

        public void Delete(FoundItemClaim entity)
        {
            context.Remove(entity);

        }

        public  async Task Delete(int id)
        {
            var foundItemClaims = await context.FoundItemClaims.SingleOrDefaultAsync(m => m.Id == id);
            context.FoundItemClaims.Remove(foundItemClaims); 
        }
        public async void Save()
        {
            await context.SaveChangesAsync();

        }

        public void Update(FoundItemClaim entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public bool FoundItemClaimsExists(int id)
        {
            return context.FoundItemClaims.Any(e => e.Id == id);

        }

        public async Task<IEnumerable<FoundItemClaim>> GetAllFoundItemClaims()
        {
            IEnumerable<FoundItemClaim> foundItemsClaims = await context.FoundItemClaims.ToListAsync();
            return foundItemsClaims;

        }

        public Task<FoundItemClaim> GetFoundItemClaimById(int? id)
        {
            var foundIteClaim = context.FoundItemClaims.FirstOrDefaultAsync(c => c.Id == id);
            return foundIteClaim;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsById(int? id)
        {
            IEnumerable<FoundItemClaim> foundItemClaims = await context.FoundItemClaims.Where(c => c.Id == id).ToListAsync();
            return foundItemClaims;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsByUser(ApplicationUser user)
        {

            IEnumerable<FoundItemClaim> foundItem = await context.FoundItemClaims.Where(c => c.ApplicationUser == user).ToListAsync();
            return foundItem;
          
        }

        
    }
}
