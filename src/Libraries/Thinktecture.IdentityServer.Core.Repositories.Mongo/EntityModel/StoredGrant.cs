using System;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class StoredGrant : Entity
    {
        public string GrantId { get; set; }
        
        public int GrantType { get; set; }

        public string Subject { get; set; }

        public string Scopes { get; set; }

        public string ClientId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expiration { get; set; }


        public DateTime? RefreshTokenExpiration { get; set; }
        public string RedirectUri { get; set; }
    }
}
