using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public interface IFoundItemClaimRepository : IRepository<FoundItemClaim>
    {
        Task<IEnumerable<FoundItemClaim>> GetAllFoundItemClaims();

        Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsById(int? id);

        Task<FoundItemClaim> GetFoundItemClaimById(int? id);

        IQueryable<FoundItemClaim> GetFilterFoundItemClaims();

        Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsByUserWithoutSoftDelete(ApplicationUser user);

        Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsbyFoundItemId(int? id);

        Task<IEnumerable<FoundItemClaim>> GetFoundItemClaimsByUser(ApplicationUser user);

        bool FoundItemClaimsExists(int id);
    }
}