using System.Linq;
using System.Text.RegularExpressions;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class StoredGrantRepository : MongoRepository<StoredGrant>, IStoredGrantRepository
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

        public override void Delete(string id)
        {
            var regex = new Regex("^" + id + "$", RegexOptions.IgnoreCase);
            var item = this.FirstOrDefault(x => regex.IsMatch(x.GrantId));
            if (item == null) return;
            base.Delete(item.Id);
        }
    }
}
