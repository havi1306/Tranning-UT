using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityModel;

namespace NetCore.API.Providers
{
    public static class PricipalExtensions
    {
        /// <summary>
        /// Get UserId
        /// </summary>
        /// <param name="principal">Principal object</param>
        /// <returns>User Id</returns>
        public static int GetUserId(this IPrincipal principal)
        {
            if (principal == null)
            {
                return -1;
            }

            var claimsPrincipal = principal as ClaimsPrincipal;

            if (claimsPrincipal != null)
            {
                foreach (var identity in claimsPrincipal.Identities)
                {
                    Claim idClaim = identity.FindFirst(JwtClaimTypes.Id);

                    if (idClaim != null)
                    {
                        return int.Parse(idClaim.Value);
                    }
                }
            }

            return -1;
        }
    }
}
