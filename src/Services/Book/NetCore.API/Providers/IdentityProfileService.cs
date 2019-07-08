using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using NetCore.Core.Entities;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.API.Providers
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public IdentityProfileService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //var sub = context.Subject.GetSubjectId();
            //var user = await this._userRepository.GetAsync(sub);
            //var principal = await _claimsFactory.CreateAsync(user);

            //var claims = principal.Claims.ToList();
            //claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            //claims.Add(new Claim(JwtClaimTypes.PreferredUserName, user.Username));

            

            //claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));

            //context.IssuedClaims = claims;


            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                //var user = await this._userRepository.GetAsync(sub);
                var user = new AppUser() { Id = 1, Username = "levinhtin", Email = "levinhtin@gmail.com", FirstName = "Tin", LastName = "Le" };
                var cp = await this.getClaims(user);

                var claims = cp.Claims;
                if (context.RequestedClaimTypes != null && context.RequestedClaimTypes.Any())
                {
                    claims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
                }

                context.IssuedClaims = claims.ToList();
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            //var user = await this._userRepository.GetAsync(sub);
            var user = await Task.FromResult<AppUser>(new AppUser() { Id = 1, Username = "levinhtin", Email = "levinhtin@gmail.com", FirstName = "Tin", LastName = "Le" });
            context.IsActive = user != null;
        }

        private async Task<ClaimsPrincipal> getClaims(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var id = new ClaimsIdentity();

            id.AddClaims(await this.GetClaimsAsync(user));

            return new ClaimsPrincipal(id);
        }

        private async Task<List<Claim>> GetClaimsAsync(AppUser user)
        {
            //Database call to get calims if needed
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.Id, user.Id.ToString()));
            claims.Add(new Claim(JwtClaimTypes.Name, user.Username));
            claims.Add(new Claim(JwtClaimTypes.PreferredUserName, user.Username));
            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));

            return await Task.FromResult<List<Claim>>(claims);
        }
    }
}
