using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                {
                    new ApiScope(name: "cartService", displayName: "Cart Service")
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    // console client credentials flow client
                    new Client
                    {
                        ClientId = "PeppershopClient",
                        ClientName = "Peppershop Client",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = { new Secret("pepperhopSecret".Sha256()) },
                        AllowedScopes = { "cartService" }
                    },

                    // interactive ASP.NET Core Web App
                    new Client
                    {
                        ClientId = "web",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,

                        // where to redirect to after login
                        RedirectUris = { "https://localhost:5002/signin-oidc" },

                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        }
                    }
                };
    }
}
