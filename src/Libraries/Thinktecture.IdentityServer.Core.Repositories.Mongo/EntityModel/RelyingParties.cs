using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class RelyingParties : Entity
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Realm { get; set; }

        public TokenType? TokenType { get; set; }
        public int TokenLifeTime { get; set; }
        
        public string ReplyTo { get; set; }

        public string EncryptingCertificate { get; set; }
        public string SymmetricSigningKey { get; set; }

        public string ExtraData1 { get; set; }
        public string ExtraData2 { get; set; }
        public string ExtraData3 { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
