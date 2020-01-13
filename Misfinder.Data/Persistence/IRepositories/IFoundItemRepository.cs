using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
   public interface IFoundItemRepository: IRepository<FoundItem>
    {
        Task<IEnumerable<FoundItem>> GetAllFoundItems();
        Task<IEnumerable<FoundItem>> GetFoundItemsById(int id);
        Task<FoundItem> GetFoundItemById(int? id);

        IQueryable<FoundItem> GetFilterFoundItems();
        Task<IEnumerable<FoundItem>> GetFoundItemsByUserWithoutSoftDelete(ApplicationUser user);

        Task<IEnumerable<FoundItem>> GetFoundItemsByUser(ApplicationUser user);
        bool FoundItemExists(int id);
        IQueryable<FoundItem> SearchFoundItem(SearchViewModel model);


    }
}
