using System;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class WSFederationConfiguration : Entity
    {
        public bool Enabled { get; set; }

        public bool EnableAuthentication { get; set; }

        public bool EnableFederation { get; set; }

        public bool EnableHrd { get; set; }

        public bool AllowReplyTo { get; set; }

        public Boolean RequireReplyToWithinRealm { get; set; }
        
        public Boolean RequireSslForReplyTo { get; set; }
    }
}
