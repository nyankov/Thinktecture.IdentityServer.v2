using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration
{
    public class KeyMaterialConfiguration : Entity<int>
    {
        public string SigningCertificateName { get; set; }
        public string DecryptionCertificateName { get; set; }
        public string RSASigningKey { get; set; }
        public string SymmetricSigningKey { get; set; }
    }
}
