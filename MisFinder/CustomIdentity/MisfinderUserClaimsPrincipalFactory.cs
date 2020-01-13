using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MisFinder.Domain.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MisFinder.CustomIdentity
{
    public class MisfinderUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public MisfinderUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> identityOptions) : base(userManager, roleManager, identityOptions)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var claimsPrincipal = await base.CreateAsync(user);
            var ideentity = claimsPrincipal.Identities.First();

            ideentity.AddClaim(new Claim("lastname", user.LastName));
            return claimsPrincipal;
        }
    }
}