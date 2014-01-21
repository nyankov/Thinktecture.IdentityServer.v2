using System;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class DiagnosticsConfiguration : Entity<int>
    {
        public Boolean EnableFederationMessageTracing { get; set; }
    }
}
