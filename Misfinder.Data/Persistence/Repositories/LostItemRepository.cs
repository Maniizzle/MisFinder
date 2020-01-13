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
    public class LostItemRepository : ILostItemRepository
    {
        private readonly MisFinderDbContext context;

        public LostItemRepository(MisFinderDbContext context)
        {
            this.context = context;
        }

        public void Create(LostItem entity)
        {
            context.LostItems.Add(entity);
        }

        public void Update(LostItem entity)
        {
            context.Update(entity);
        }

        public async Task Delete(int id)
        {
            var lostItem = await context.LostItems.SingleOrDefaultAsync(m => m.Id == id);
            context.LostItems.Remove(lostItem);
        }

        public void Delete(LostItem entity)
        {
            context.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task<IEnumerable<LostItem>> GetAllLostItems()
        {
            IEnumerable<LostItem> lostItems = await context.LostItems.OrderBy(c => c.DateMisplaced).
                                                Include(c => c.LostItemClaims).ToListAsync();
            return lostItems;
        }

        public IQueryable<LostItem> GetFilterLostItems()
        {
            var query = context.LostItems.Include(c => c.LostItemClaims)
                              .Include(c => c.LostItemUser).
                              Include(c => c.LocalGovernment)
                              .ThenInclude(c => c.State)
                              ;

            return query;
        }

        public async Task<IEnumerable<LostItem>> GetLostItemsById(int? id)
        {
            IEnumerable<LostItem> lostItems = await context.LostItems.Include(c => c.LostItemClaims)
                            .Include(c => c.LocalGovernment)
                          .Where(c => c.Id == id).ToListAsync();
            return lostItems;
        }

        public Task<LostItem> GetLostItemById(int? id)
        {
            var lostItem = context.LostItems.
                            Include(c => c.Image)
                            .Include(c => c.LostItemUser)
                            .Include(c => c.LostItemClaims)
                            .Include(c => c.LocalGovernment)
                            .ThenInclude(b => b.State).FirstOrDefaultAsync(c => c.Id == id);
            return lostItem;
        }

        public async Task<IEnumerable<LostItem>> GetLostItemsByUser(ApplicationUser user)
        {
            IEnumerable<LostItem> lostItem = await context.LostItems
                            .Include(c => c.LostItemClaims)
                            .Include(c => c.LocalGovernment)
                            .ThenInclude(b => b.State)
                            .Where(c => (c.LostItemUser == user && c.IsDeleted == false)).ToListAsync();
            return lostItem;
        }

        public async Task<IEnumerable<LostItem>> GetLostItemsByUserWithoutSoftDelete(ApplicationUser user)
        {
            IEnumerable<LostItem> lostItem = await context.LostItems
                            .Include(c => c.LostItemClaims)
                            .Include(c => c.LocalGovernment)
                            .ThenInclude(b => b.State)
                            .Where(c => c.LostItemUser == user).ToListAsync();
            return lostItem;
        }

        public Task<LostItem> GetLostItemByUser(ApplicationUser user)
        {
            var lostItem = context.LostItems.FirstOrDefaultAsync(c => c.LostItemUser == user);
            return lostItem;
        }

        public bool LostItemExists(int id)
        {
            return context.FoundItems.Any(e => e.Id == id);
        }

        public IQueryable<LostItem> SearchLostItem(SearchViewModel model)
        {
            //IQueryable<LostItem> result = new List<LostItem>();
            if (!string.IsNullOrEmpty(model.Name))
            {
                var result = context.LostItems.Include(c => c.LocalGovernment).Include(c => c.Image).
                       Where(c => (((model.Date >= c.DateMisplaced.AddDays(-2)) && (model.Date <= c.DateMisplaced.AddDays(5)))
                       && (c.LocalGovernment.StateId == model.StateId) && c.IsFound == false))
                      .OrderBy(c => c.DateMisplaced)
                      .ThenBy(c => c.NameOfLostItem)
                      .Select(s => new LostItem
                      {
                          Id = s.Id,
                          NameOfLostItem = s.NameOfLostItem,
                          Description = s.Description,
                          ExactArea = s.ExactArea,
                          Image = s.Image,
                          ItemCategory = s.ItemCategory,
                          DateMisplaced = s.DateMisplaced
                      });
                return result;
            }
            return null;
        }
    }
}