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
            context.Entry(entity).State = EntityState.Modified;
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
        public async void Save()
        {
           await context.SaveChangesAsync();
        }



        public async Task<IEnumerable<LostItem>> GetAllLostItems()
        {
            IEnumerable<LostItem> lostItems = await context.LostItems.ToListAsync();
            return lostItems;
           
        }


        public async Task<IEnumerable<LostItem>> GetLostItemsById(int id)
        {
            IEnumerable<LostItem> lostItems = await context.LostItems.Where(c=>c.Id==id).ToListAsync();
            return lostItems;


        }

        public Task<LostItem> GetLostItemById(int id)
        {
            var lostItem = context.LostItems.FirstOrDefaultAsync(c => c.Id == id);
            return lostItem;
        }
        public async Task<IEnumerable<LostItem>> GetLostItemsByUser(ApplicationUser user)
        {
            IEnumerable<LostItem> lostItem = await context.LostItems.Where(c => c.LostItemUser == user).ToListAsync();
            return lostItem;
        }

        public Task<LostItem> GetLostItemByUser(ApplicationUser user)
        {
            var lostItem =  context.LostItems.FirstOrDefaultAsync(c => c.LostItemUser == user);
            return lostItem;

        }

        public bool FoundItemExists(int id)
        {
            return context.FoundItems.Any(e => e.Id == id);   
        }
    }
}
