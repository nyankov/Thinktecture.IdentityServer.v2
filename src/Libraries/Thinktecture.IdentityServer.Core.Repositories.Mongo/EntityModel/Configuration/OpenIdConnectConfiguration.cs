using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class OpenIdConnectConfiguration : Entity
    {
        public bool Enabled { get; set; }
    }
}
