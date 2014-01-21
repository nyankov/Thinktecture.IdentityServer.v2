using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class OAuth2Configuration : Entity<int>
    {
        public bool Enabled { get; set; }

        public bool EnableConsent { get; set; }
        public bool EnableResourceOwnerFlow { get; set; }
        public bool EnableImplicitFlow { get; set; }
        public bool EnableCodeFlow { get; set; }
    }
}
