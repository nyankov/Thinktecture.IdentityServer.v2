using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    internal class GlobalConfigurationRepository : MongoRepository<GlobalConfiguration>
    {
        public GlobalConfigurationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }
    }

    internal class DiagnosticsConfigurationRepository : MongoRepository<DiagnosticsConfiguration>
    {
        public DiagnosticsConfigurationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class KeyMaterialConfigurationRepository : MongoRepository<KeyMaterialConfiguration>
    {
        public KeyMaterialConfigurationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class WSFederationRepository : MongoRepository<WSFederationConfiguration>
    {
        public WSFederationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class FederationMetadataRepository : MongoRepository<FederationMetadataConfiguration>
    {
        public FederationMetadataRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class WSTrustRepository : MongoRepository<WSTrustConfiguration>
    {
        public WSTrustRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class OAuth2Repository : MongoRepository<OAuth2Configuration>
    {
        public OAuth2Repository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class SimpleHttpRepository : MongoRepository<SimpleHttpConfiguration>
    {
        public SimpleHttpRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class AdfsIntegrationRepository : MongoRepository<AdfsIntegrationConfiguration>
    {
        public AdfsIntegrationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }

    internal class OpenIdConnectRepository : MongoRepository<OpenIdConnectConfiguration>
    {
        public OpenIdConnectRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }
    }
}