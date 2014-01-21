using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class SimpleHttpConfiguration : Entity<int>
    {
        public bool Enabled { get; set; }
    }
}
