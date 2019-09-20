using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
   public interface ILostItemRepository:IRepository<LostItem>
    {
        Task<IEnumerable<LostItem>> GetAllLostItems();
        Task<IEnumerable<LostItem>> GetLostItemsById(int id);
        Task<LostItem> GetLostItemById(int id);

        Task<IEnumerable<LostItem>> GetLostItemsByUser(ApplicationUser user);
        bool FoundItemExists(int id);
       


    }
}
