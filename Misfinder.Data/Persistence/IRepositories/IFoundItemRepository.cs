using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
   public interface IFoundItemRepository: IRepository<FoundItem>
    {
        Task<IEnumerable<FoundItem>> GetAllFoundItems();
        Task<IEnumerable<FoundItem>> GetFoundItemsById(int id);
        Task<FoundItem> GetFoundItemById(int? id);

        Task<IEnumerable<FoundItem>> GetFoundItemsByUser(ApplicationUser user);
        bool FoundItemExists(int id);


    }
}
