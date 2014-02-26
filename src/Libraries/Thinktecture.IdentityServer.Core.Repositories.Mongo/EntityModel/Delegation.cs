using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class Delegation : Entity
    {
        public string UserName { get; set; }
        public string Realm { get; set; }
        public string Description { get; set; }
    }
}
