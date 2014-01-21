using System;
using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class RelyingPartyRepository : MongoRepository<RelyingParties, int>, IRelyingPartyRepository
    {
        public RelyingPartyRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }

        public bool TryGet(string realm, out RelyingParty relyingParty)
        {
            relyingParty = null;

                var match = this.Where(rp => rp.Realm.Equals(realm, StringComparison.OrdinalIgnoreCase) &&
                                              rp.Enabled)
                                .OrderByDescending(rp => rp.Realm)
                                .FirstOrDefault();

                if (match != null)
                {
                    relyingParty = match.ToDomainModel();
                    return true;
                }
            return false;
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
            var uniqueId = int.Parse(id);

            return this.First(rp => rp.Id == uniqueId).ToDomainModel();

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

        public void Delete(string id)
        {
            var rpEntity = new RelyingParties {Id = int.Parse(id)};
            var rp = this.FirstOrDefault(r => r.Id == rpEntity.Id);
            if (rp == null) return;
            Delete(rp);
        }

        #endregion
    }
}
