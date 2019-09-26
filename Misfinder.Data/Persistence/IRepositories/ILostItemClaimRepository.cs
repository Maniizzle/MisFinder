using MisFinder.Domain.Models;
using MisFinder.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
   public interface ILostItemClaimRepository:IRepository<LostItemClaim>
    {
        Task<IEnumerable<LostItemClaim>> GetAllLostItemClaims();
        Task<IEnumerable<LostItemClaim>> GetLostItemClaimsById(int? id);
        Task<LostItemClaim> GetLostItemClaimById(int? id);

        Task<IEnumerable<LostItemClaim>> GetLostItemClaimsByUser(ApplicationUser user);
        bool LostItemClaimsExists(int id);

    }
}
