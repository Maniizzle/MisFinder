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

        public async Task Delete(int id)
        {
            var lostItemClaims = await context.LostItemClaims.SingleOrDefaultAsync(m => m.Id == id);
            context.LostItemClaims.Remove(lostItemClaims);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(LostItemClaim entity)
        {
            context.Update(entity);
        }

        public IQueryable<LostItemClaim> GetFilterLostItemClaims()
        {
            var query = context.LostItemClaims.Include(c => c.LostItem).
                                ThenInclude(c => c.LostItemUser)
                              .Include(c => c.ApplicationUser).
                              Include(c => c.Image);
            return (query);
        }

        public async Task<IEnumerable<LostItemClaim>> GetLostItemClaimsbyLostItemId(int? id)
        {
            var claims = await context.LostItemClaims
                .Include(c => c.Image)
                .Include(c => c.ApplicationUser).
                Where(c => c.LostItemId == id).ToListAsync();
            return claims;
        }

        public async Task<IEnumerable<LostItemClaim>> GetAllLostItemClaims()
        {
            IEnumerable<LostItemClaim> lostItemClaims = await context.LostItemClaims.Distinct().ToListAsync();
            return lostItemClaims;
        }

        public Task<LostItemClaim> GetLostItemClaimById(int? id)
        {
            // var claim = context.LostItemClaims.FirstOrDefault(c => c.Id == id);
            //  if claim.
            var lostItemClaim = context.LostItemClaims
                            .Include(c => c.ApplicationUser)
                            .Include(c => c.LostItem).ThenInclude(c => c.LostItemUser)
                            .Include(c => c.LostItem).ThenInclude(c => c.Image)
                            .Include(c => c.Image)
                            .FirstOrDefaultAsync(c => c.Id == id);
            return lostItemClaim;
        }

        public async Task<IEnumerable<LostItemClaim>> GetLostItemClaimsById(int? id)
        {
            IEnumerable<LostItemClaim> lostItemClaims = await context.LostItemClaims.Where(c => c.Id == id).ToListAsync();
            return lostItemClaims;
        }

        public async Task<IEnumerable<LostItemClaim>> GetLostItemClaimsByUser(ApplicationUser user)
        {
            IEnumerable<LostItemClaim> lostItem = await context.LostItemClaims.Where(c => c.ApplicationUser == user && c.IsDeleted == false).ToListAsync();
            return lostItem;
        }

        public async Task<IEnumerable<LostItemClaim>> GetLostItemClaimsByUserWithoutSoftDelete(ApplicationUser user)
        {
            IEnumerable<LostItemClaim> lostItemClaims = await context.LostItemClaims
                            .Where(c => c.ApplicationUser == user).ToListAsync();
            return lostItemClaims;
        }

        public bool LostItemClaimsExists(int id)
        {
            return context.LostItemClaims.Any(e => e.Id == id);
        }
    }
}