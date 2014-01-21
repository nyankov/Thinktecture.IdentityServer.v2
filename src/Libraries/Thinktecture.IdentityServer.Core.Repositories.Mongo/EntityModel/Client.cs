using Thinktecture.IdentityServer.Repositories.Mongo.Data;

namespace Thinktecture.IdentityServer.Repositories.Mongo.EntityModel
{
    public class Client : Entity<int>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public bool AllowRefreshToken { get; set; }

        public bool AllowImplicitFlow { get; set; }

        public bool AllowResourceOwnerFlow { get; set; }

        public bool AllowCodeFlow { get; set; }
    }
}