using System;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class GlobalConfiguration : Entity
    {
        public String SiteName { get; set; }
        
        public String IssuerUri { get; set; }

        public String IssuerContactEmail { get; set; }

        public string DefaultWSTokenType { get; set; }

        public string DefaultHttpTokenType { get; set; }

        public int DefaultTokenLifetime { get; set; }

        public int MaximumTokenLifetime { get; set; }

        public int SsoCookieLifetime { get; set; }

        public Boolean RequireEncryption { get; set; }

        public Boolean RequireRelyingPartyRegistration { get; set; }

        public Boolean EnableClientCertificateAuthentication { get; set; }

        public Boolean EnforceUsersGroupMembership { get; set; }

        public int HttpPort { get; set; }

        public int HttpsPort { get; set; }

        public bool DisableSSL { get; set; }

        public string PublicHostName { get; set; }
    }
}
