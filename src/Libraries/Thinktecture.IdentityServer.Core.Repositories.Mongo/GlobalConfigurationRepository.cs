using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    internal class GlobalConfigurationRepository : MongoRepository<GlobalConfiguration, int>
    {
        public GlobalConfigurationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }
    }

    internal class DiagnosticsConfigurationRepository : MongoRepository<DiagnosticsConfiguration, int>
    {
        public DiagnosticsConfigurationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class KeyMaterialConfigurationRepository : MongoRepository<KeyMaterialConfiguration, int>
    {
        public KeyMaterialConfigurationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class WSFederationRepository : MongoRepository<WSFederationConfiguration, int>
    {
        public WSFederationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class FederationMetadataRepository : MongoRepository<FederationMetadataConfiguration, int>
    {
        public FederationMetadataRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class WSTrustRepository : MongoRepository<WSTrustConfiguration, int>
    {
        public WSTrustRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class OAuth2Repository : MongoRepository<OAuth2Configuration, int>
    {
        public OAuth2Repository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class SimpleHttpRepository : MongoRepository<SimpleHttpConfiguration, int>
    {
        public SimpleHttpRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class AdfsIntegrationRepository : MongoRepository<AdfsIntegrationConfiguration, int>
    {
        public AdfsIntegrationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class OpenIdConnectRepository : MongoRepository<OpenIdConnectConfiguration, int>
    {
        public OpenIdConnectRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }
}