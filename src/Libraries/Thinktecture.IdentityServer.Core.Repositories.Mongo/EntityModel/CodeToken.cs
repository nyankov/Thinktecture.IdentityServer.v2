using System;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class CodeToken : Entity
    {
        public string Code { get; set; }

        public string ClientId { get; set; }

        public string UserName { get; set; }

        public string Scope { get; set; }

        public int Type { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
