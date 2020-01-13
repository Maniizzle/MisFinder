using MisFinder.Domain.Models;
using MisFinder.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public interface ILostItemClaimRepository : IRepository<LostItemClaim>
    {
        Task<IEnumerable<LostItemClaim>> GetAllLostItemClaims();

        Task<IEnumerable<LostItemClaim>> GetLostItemClaimsById(int? id);

        Task<LostItemClaim> GetLostItemClaimById(int? id);

        IQueryable<LostItemClaim> GetFilterLostItemClaims();

        Task<IEnumerable<LostItemClaim>> GetLostItemClaimsByUserWithoutSoftDelete(ApplicationUser user);

        Task<IEnumerable<LostItemClaim>> GetLostItemClaimsByUser(ApplicationUser user);

        bool LostItemClaimsExists(int id);

        Task<IEnumerable<LostItemClaim>> GetLostItemClaimsbyLostItemId(int? id);
    }
}