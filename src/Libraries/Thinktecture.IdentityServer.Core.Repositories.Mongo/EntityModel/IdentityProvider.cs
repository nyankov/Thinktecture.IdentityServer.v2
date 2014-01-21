using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class IdentityProvider : Entity<int>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Type { get; set; }
        public bool ShowInHrdSelection { get; set; }
        public string WSFederationEndpoint { get; set; }
        public string IssuerThumbprint { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public int? OAuth2ProviderType { get; set; }
        public bool Enabled { get; set; }
    }
}