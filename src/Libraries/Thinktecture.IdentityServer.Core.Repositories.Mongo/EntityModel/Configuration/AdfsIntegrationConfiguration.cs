using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class AdfsIntegrationConfiguration:Entity
    {
        public bool Enabled { get; set; }

        // general settings - authentication
        public bool UsernameAuthenticationEnabled { get; set; }
        public bool SamlAuthenticationEnabled { get; set; }
        public bool JwtAuthenticationEnabled { get; set; }
        public bool PassThruAuthenticationToken { get; set; }
        public int AuthenticationTokenLifetime { get; set; }

        // adfs settings
        public string UserNameAuthenticationEndpoint { get; set; }
        public string FederationEndpoint { get; set; }
        public string IssuerUri { get; set; }
        public string IssuerThumbprint { get; set; }
        public string EncryptionCertificate { get; set; }
    }
}
