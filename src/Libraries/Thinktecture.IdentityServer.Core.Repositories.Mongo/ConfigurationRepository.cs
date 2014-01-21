using System.Linq;
using Thinktecture.IdentityServer.Models.Configuration;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly GlobalConfigurationRepository _globalConfigurationRepository =
            new GlobalConfigurationRepository();

        private readonly DiagnosticsConfigurationRepository _diagnosticsConfigurationRepository =
            new DiagnosticsConfigurationRepository();

        private readonly KeyMaterialConfigurationRepository _keyMaterialConfigurationRepository =
            new KeyMaterialConfigurationRepository();

        private readonly WSFederationRepository _wsFederationRepository = new WSFederationRepository();
        private readonly FederationMetadataRepository _federationMetadataRepository = new FederationMetadataRepository();
        private readonly WSTrustRepository _wsTrustRepository = new WSTrustRepository();
        private readonly OAuth2Repository _oAuth2Repository = new OAuth2Repository();
        private readonly SimpleHttpRepository _simpleHttpRepository = new SimpleHttpRepository();
        private readonly AdfsIntegrationRepository _adfsIntegrationRepository = new AdfsIntegrationRepository();
        private readonly OpenIdConnectRepository _openIdConnectRepository = new OpenIdConnectRepository();



        public virtual bool SupportsWriteAccess
        {
            get { return true; }
        }

        public virtual Models.Configuration.GlobalConfiguration Global
        {
            get
            {

                var entity = _globalConfigurationRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _globalConfigurationRepository.FirstOrDefault();
                if (entity!=null)
                    _globalConfigurationRepository.Delete(entity);

                _globalConfigurationRepository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.DiagnosticsConfiguration Diagnostics
        {
            get
            {
                var entity = _diagnosticsConfigurationRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _diagnosticsConfigurationRepository.FirstOrDefault();
                if (entity != null)
                    _diagnosticsConfigurationRepository.Delete(entity);

                _diagnosticsConfigurationRepository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.KeyMaterialConfiguration Keys
        {
            get
            {
                var entity = _keyMaterialConfigurationRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : new KeyMaterialConfiguration();
            }
            set
            {
                var entity = _keyMaterialConfigurationRepository.FirstOrDefault();
                if (entity != null)
                    _keyMaterialConfigurationRepository.Delete(entity);
               
                _keyMaterialConfigurationRepository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.WSFederationConfiguration WSFederation
        {
            get
            {
                var entity = _wsFederationRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _wsFederationRepository.FirstOrDefault();
                if (entity != null)
                    _wsFederationRepository.Delete(entity);

                _wsFederationRepository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.FederationMetadataConfiguration FederationMetadata
        {
            get
            {
                var entity = _federationMetadataRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;

            }
            set
            {
                var entity = _federationMetadataRepository.FirstOrDefault();
                if (entity != null)
                    _federationMetadataRepository.Delete(entity);

                _federationMetadataRepository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.WSTrustConfiguration WSTrust
        {
            get
            {
                var entity = _wsTrustRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _wsTrustRepository.FirstOrDefault();
                if (entity != null)
                    _wsTrustRepository.Delete(entity);

                _wsTrustRepository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.OAuth2Configuration OAuth2
        {
            get
            {
                var entity = _oAuth2Repository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _oAuth2Repository.FirstOrDefault();
                if (entity != null)
                    _oAuth2Repository.Delete(entity);

                _oAuth2Repository.Add(value.ToEntity());
            }
        }

        public virtual Models.Configuration.SimpleHttpConfiguration SimpleHttp
        {
            get
            {
                var entity = _simpleHttpRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _simpleHttpRepository.FirstOrDefault();
                if (entity != null)
                    _simpleHttpRepository.Delete(entity);

                _simpleHttpRepository.Add(value.ToEntity());
            }
        }

        public Models.Configuration.AdfsIntegrationConfiguration AdfsIntegration
        {
            get
            {
                var entity = _adfsIntegrationRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _adfsIntegrationRepository.FirstOrDefault();
                if (entity != null)
                    _adfsIntegrationRepository.Delete(entity);

                _adfsIntegrationRepository.Add(value.ToEntity());
            }
        }


        public Models.Configuration.OpenIdConnectConfiguration OpenIdConnect
        {
            get
            {
                var entity = _openIdConnectRepository.FirstOrDefault();
                return entity != null ? entity.ToDomainModel() : null;
            }
            set
            {
                var entity = _openIdConnectRepository.FirstOrDefault();
                if (entity != null)
                    _openIdConnectRepository.Delete(entity);

                _openIdConnectRepository.Add(value.ToEntity());
            }
        }
    }
}
