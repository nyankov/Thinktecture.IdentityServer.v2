using System.Collections.Generic;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class OpenIdConnectClientEntity : Entity<int>
    {
        public OpenIdConnectClientEntity()
        {
            RedirectUris = new HashSet<OpenIdConnectClientRedirectUri>();
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public ClientSecretTypes ClientSecretType { get; set; }
        public string Name { get; set; }

        // openid connect
        public OpenIdConnectFlows Flow { get; set; }
        public bool AllowRefreshToken { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
        public bool RequireConsent { get; set; }
        public ICollection<OpenIdConnectClientRedirectUri> RedirectUris { get; set; }
    }
}