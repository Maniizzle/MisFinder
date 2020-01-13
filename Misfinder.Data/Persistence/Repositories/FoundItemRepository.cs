using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.Repositories
{
    public class FoundItemRepository : IFoundItemRepository
    {
        private readonly MisFinderDbContext context;

        public FoundItemRepository(MisFinderDbContext context)
        {
            this.context = context;
        }

        public void Create(FoundItem entity)
        {
            context.FoundItems.Add(entity);
        }

        public void Update(FoundItem entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var FoundItem = await context.FoundItems.SingleOrDefaultAsync(m => m.Id == id);
            context.FoundItems.Remove(FoundItem);
        }

        public void Delete(FoundItem entity)
        {
            context.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IQueryable<FoundItem> GetFilterFoundItems()
        {
            var query = context.FoundItems.Include(c => c.FoundItemClaims)
                              .Include(c => c.FoundItemUser).
                              Include(c => c.LocalGovernment).
                              ThenInclude(c => c.State);
            return query;
        }

        public async Task<IEnumerable<FoundItem>> GetAllFoundItems()
        {
            IEnumerable<FoundItem> FoundItems = await context.FoundItems.ToListAsync();
            return FoundItems;
        }

        public async Task<IEnumerable<FoundItem>> GetFoundItemsById(int id)
        {
            IEnumerable<FoundItem> foundItems = await context.FoundItems.Where(c => c.Id == id).ToListAsync();
            return foundItems;
        }

        public Task<FoundItem> GetFoundItemById(int? id)
        {
            var foundItem = context.FoundItems.Include(c => c.FoundItemClaims)
                            .Include(c => c.LocalGovernment)
                            .Include(c => c.Image)
                          .SingleOrDefaultAsync(c => c.Id == id);
            return foundItem;
        }

        public async Task<IEnumerable<FoundItem>> GetFoundItemsByUser(ApplicationUser user)
        {
            IEnumerable<FoundItem> foundItem = new List<FoundItem>();
            foundItem = await context.FoundItems
                          .Include(c => c.FoundItemClaims)
                          .Include(c => c.LocalGovernment)
                        .Where(c => c.FoundItemUser == user && c.IsDeleted == false).ToListAsync();
            return foundItem;
        }

        public async Task<IEnumerable<FoundItem>> GetFoundItemsByUserWithoutSoftDelete(ApplicationUser user)
        {
            IEnumerable<FoundItem> foundItem = await context.FoundItems
                            .Include(c => c.FoundItemClaims)
                            .Include(c => c.LocalGovernment)
                            .ThenInclude(b => b.State)
                            .Where(c => c.FoundItemUser == user).ToListAsync();
            return foundItem;
        }

        public Task<FoundItem> GetFoundItemByUser(ApplicationUser user)
        {
            var foundItem = context.FoundItems.FirstOrDefaultAsync(c => c.FoundItemUser == user);
            return foundItem;
        }

        public bool FoundItemExists(int id)
        {
            return context.FoundItems.Any(e => e.Id == id);
        }

        public IQueryable<FoundItem> SearchFoundItem(SearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                var result = context.FoundItems.Include(c => c.LocalGovernment)
                   .ThenInclude(c => c.State)
                  .Where(c => (
                               (c.LocalGovernment.StateId == model.StateId) && (c.DateFound <= model.Date.AddDays(6) && model.Date >= c.DateFound.AddDays(-2))))
                  .OrderBy(c => c.NameOfFoundItem).ThenBy(c => c.ItemCategory);
                return result;
            }

            return null;
        } //var result = new List<FoundItem>();

        //return result;
    }
}