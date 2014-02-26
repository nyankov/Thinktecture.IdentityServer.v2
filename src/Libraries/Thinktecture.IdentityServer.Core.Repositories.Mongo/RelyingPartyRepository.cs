using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;


namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class RelyingPartyRepository : MongoRepository<RelyingParties>, IRelyingPartyRepository
    {
        public RelyingPartyRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }

        public bool TryGet(string realm, out RelyingParty relyingParty)
        {
            relyingParty = null;
            var regex = new Regex("^" + realm + "$", RegexOptions.IgnoreCase);
            var match = this.Where(rp => regex.IsMatch(rp.Realm) && rp.Enabled)
                .OrderByDescending(rp => rp.Realm)
                .FirstOrDefault();

            if (match == null) return false;
            relyingParty = match.ToDomainModel();

            return true;
        }

        #region Management
        public bool SupportsWriteAccess
        {
            get { return true; }
        }

        public IEnumerable<RelyingParty> List(int pageIndex, int pageSize)
        {
            var rps = this.OrderBy(e => e.Name);

            if (pageIndex != -1 && pageSize != -1)
            {
                rps = rps.Skip(pageIndex*pageSize).Take(pageSize).OrderBy(rp => rp.Name);
            }

            return rps.ToList().ToDomainModel();
        }

        public RelyingParty Get(string id)
        {
            return this.First(rp => rp.Id == id).ToDomainModel();

        }

        public void Add(RelyingParty relyingParty)
        {
            Add(relyingParty.ToEntity());
        }

        public void Update(RelyingParty relyingParty)
        {
            var rpEntity = relyingParty.ToEntity();
            Update(rpEntity);
        }

        public override void Delete(string id)
        {
            var rpEntity = new RelyingParties {Id = id};
            var rp = this.FirstOrDefault(r => r.Id == rpEntity.Id);
            if (rp == null) return;
            base.Delete(rp.Id);
        }

        public RelyingParty GetByRealm(string realm)
        {
            return this.FirstOrDefault(rp => rp.Realm == realm).ToDomainModel();
        }

        #endregion
    }
}
