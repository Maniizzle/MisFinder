using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Delete(int id)
        {
            var foundItemClaims = await context.FoundItemClaims.SingleOrDefaultAsync(m => m.Id == id);
            context.FoundItemClaims.Remove(foundItemClaims);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(FoundItemClaim entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public bool FoundItemClaimsExists(int id)
        {
            return context.FoundItemClaims.Any(e => e.Id == id);
        }

        public IQueryable<FoundItemClaim> GetFilterFoundItemClaims()
        {
            var query = context.FoundItemClaims.Include(c => c.FoundItem).
                                ThenInclude(c => c.FoundItemUser)
                              .Include(c => c.ApplicationUser).
                              Include(c => c.Image);
            return query;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetAllFoundItemClaims()
        {
            IEnumerable<FoundItemClaim> foundItemsClaims = await context.FoundItemClaims.ToListAsync();
            return foundItemsClaims;
        }

        public Task<FoundItemClaim> GetFoundItemClaimById(int? id)
        {
            var foundIteClaim = context.FoundItemClaims.
                Include(c => c.Image).
                Include(c => c.ApplicationUser).
                Include(c => c.FoundItem).ThenInclude(c => c.Image).
               Include(c => c.FoundItem).ThenInclude(c => c.FoundItemUser).Include(c => c.FoundItem).ThenInclude(c => c.LocalGovernment).ThenInclude(c => c.State)
                .FirstOrDefaultAsync(c => (c.Id == id) && (c.FoundItem.IsDeleted == false));
            return foundIteClaim;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsById(int? id)
        {
            IEnumerable<FoundItemClaim> foundItemClaims = await context.FoundItemClaims.Where(c => c.Id == id).ToListAsync();
            return foundItemClaims;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsByUser(ApplicationUser user)
        {
            IEnumerable<FoundItemClaim> foundItemClaims = await context.FoundItemClaims.
                Include(c => c.FoundItem).Where(c => c.ApplicationUser == user && c.IsDeleted == false).ToListAsync();
            return foundItemClaims;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsByUserWithoutSoftDelete(ApplicationUser user)
        {
            var foundItemClaims = await context.FoundItemClaims.Where(c => c.ApplicationUser == user).ToListAsync();
            return foundItemClaims;
        }

        public async Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsbyFoundItemId(int? id)
        {
            var claims = await context.FoundItemClaims
                 .Include(c => c.Image)
                .Include(c => c.ApplicationUser).
                Where(c => c.FoundItemId == id).ToListAsync();
            return claims;
        }
    }
}