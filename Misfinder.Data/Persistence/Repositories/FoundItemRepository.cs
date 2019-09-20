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
   public class FoundItemRepository: IFoundItemRepository
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
        public async void Save()
        {
           await context.SaveChangesAsync();
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
            var foundItem = context.FoundItems.SingleOrDefaultAsync(c => c.Id == id);
            return foundItem;
        }
        public async Task<IEnumerable<FoundItem>> GetFoundItemsByUser(ApplicationUser user)
        {
            IEnumerable<FoundItem> FoundItem = await context.FoundItems.Where(c => c.FoundItemUser == user).ToListAsync();
            return FoundItem;
        }

        public Task<FoundItem> GetFoundItemByUser(ApplicationUser user)
        {
            var FoundItem = context.FoundItems.FirstOrDefaultAsync(c => c.FoundItemUser == user);
            return FoundItem;

        }

        public bool FoundItemExists(int id)
        {
            return context.FoundItems.Any(e => e.Id == id);
        }
    }
}
