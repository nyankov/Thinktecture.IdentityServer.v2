using System;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class WSTrustConfiguration : Entity
    {
        public bool Enabled { get; set; }

        public bool EnableMessageSecurity { get; set; }
        
        public bool EnableMixedModeSecurity { get; set; }
        
        public bool EnableClientCertificateAuthentication { get; set; }
        
        public bool EnableFederatedAuthentication { get; set; }
        
        public Boolean EnableDelegation { get; set; }
    }
}
