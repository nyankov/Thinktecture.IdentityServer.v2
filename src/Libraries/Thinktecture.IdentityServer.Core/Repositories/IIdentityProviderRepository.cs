using System.Collections.Generic;
using Thinktecture.IdentityServer.Models;

namespace Thinktecture.IdentityServer.Repositories
{
    public interface IIdentityProviderRepository
    {
        IEnumerable<IdentityProvider> GetAll();
        IdentityProvider Get(string id);
        void Add(IdentityProvider item);
        void Update(IdentityProvider item);
        void Delete(string id);
        bool TryGet(string name, out IdentityProvider identityProvider);
    }
}
