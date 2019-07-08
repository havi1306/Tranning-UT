using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.API
{
    using System.Security.Claims;
    using IdentityModel;
    using IdentityServer4;
    using IdentityServer4.Models;

    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                //new IdentityResource("audiobooks", "AudioBooks" ,new []{ "role", "admin", "user", "audiobooks", "audiobooks.admin", "audiobooks.user" } ),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //new ApiResource("audiobooks")
                //{
                //    ApiSecrets =
                //    {
                //        new Secret("audiobook.secret".Sha256())
                //    },
                //    Scopes =
                //    {
                //        new Scope
                //        {
                //            Name = "audiobooks",
                //            DisplayName = "Scope for the AudioBooks ApiResource"
                //        }
                //    },
                //    UserClaims = { "role", "admin", "user", "audiobooks", "audiobooks.admin", "audiobooks.user" }
                //}
                new ApiResource("audiobookapp", "AudioBook App", new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, JwtClaimTypes.Email, JwtClaimTypes.Id, JwtClaimTypes.PreferredUserName }),
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "audiobook.client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("audiobook.secret".Sha256())
                    },
                    AllowedScopes = { "audiobookapp" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "audiobook.ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    ClientSecrets =
                    {
                        new Secret("audiobook.secret".Sha256())
                    },
                    AllowedScopes = { "audiobookapp", "offline_access" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 2592000
                },

                // OpenID Connect implicit flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}
