using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using NetCore.Infrastructure.Repositories.Interfaces;

namespace NetCore.API.Providers
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;
        public IdentityResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await this._userRepository.CheckPasswordAsync(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult(context.UserName, OidcConstants.AuthenticationMethods.Password);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient);
            }

            return;

        }
    }
}
