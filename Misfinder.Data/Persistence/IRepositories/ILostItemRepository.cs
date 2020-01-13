using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
   public interface ILostItemRepository:IRepository<LostItem>
    {
        Task<IEnumerable<LostItem>> GetAllLostItems();
        Task<IEnumerable<LostItem>> GetLostItemsById(int? id);
        Task<LostItem> GetLostItemById(int? id);
        IQueryable<LostItem> GetFilterLostItems();
        Task<IEnumerable<LostItem>> GetLostItemsByUserWithoutSoftDelete(ApplicationUser user);
        Task<IEnumerable<LostItem>> GetLostItemsByUser(ApplicationUser user);
        bool LostItemExists(int id);
        IQueryable<LostItem> SearchLostItem(SearchViewModel model);

    }
}
