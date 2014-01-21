using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class StoredGrantRepository : MongoRepository<StoredGrant, int>, IStoredGrantRepository
    {
        public StoredGrantRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }

        public void Add(Models.StoredGrant grant)
        {
            var entity = grant.ToEntityModel();
            Add(entity);
        }

        public Models.StoredGrant Get(string id)
        {
            var result = this.SingleOrDefault(sg => sg.GrantId == id);

            return result != null ? result.ToDomainModel() : null;
        }

        public void Delete(string id)
        {
            var item = this.FirstOrDefault(x => x.GrantId.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (item == null) return;
            Delete(item);
        }
    }
}
