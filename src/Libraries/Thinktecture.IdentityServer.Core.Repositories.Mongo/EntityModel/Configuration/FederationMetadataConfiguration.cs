using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class FederationMetadataConfiguration : Entity<int>
    {
        public bool Enabled { get; set; }
    }
}
