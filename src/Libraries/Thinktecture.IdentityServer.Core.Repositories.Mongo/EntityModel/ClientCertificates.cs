using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class ClientCertificates : Entity
    {
        public string UserName { get; set; }
        
        public string Thumbprint { get; set; }
        
        public string Description { get; set; }
    }
}
