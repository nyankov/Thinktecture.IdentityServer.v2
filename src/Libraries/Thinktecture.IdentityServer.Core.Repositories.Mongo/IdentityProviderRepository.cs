using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class IdentityProviderRepository : MongoRepository<IdentityProvider, int>,IIdentityProviderRepository
    {
        public IdentityProviderRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }

        IEnumerable<Models.IdentityProvider> IIdentityProviderRepository.GetAll()
        {
           return this.ToList().ToDomainModel();
        }

        public bool TryGet(string name, out Models.IdentityProvider identityProvider)
        {
            identityProvider = this.FirstOrDefault(idp => idp.Name == name).ToDomainModel();
            return (identityProvider != null);
        }

        public void Add(Models.IdentityProvider item)
        {
            ValidateUniqueName(item);
            var entity = item.ToEntity();
            Add(entity);
            item.ID = entity.Id;
        }

        private void ValidateUniqueName(Models.IdentityProvider item)
        {
            var othersWithSameName = this.Where(e => e.Name == item.Name && e.Id != item.ID);
            if (othersWithSameName.Any()) throw new ValidationException(string.Format(Resources.IdentityProviderRepository.NameAlreadyInUseError, item.Name));
        }

        public override void Delete(int id)
        {
            var item = this.FirstOrDefault(idp => idp.Id == id);
            if (item == null) return;
            Delete(item);
        }

        public void Update(Models.IdentityProvider item)
        {
            ValidateUniqueName(item);

            var dbitem = this.FirstOrDefault(idp => idp.Id == item.ID);
            if (dbitem == null) return;
            item.UpdateEntity(dbitem);
            Update(dbitem);
        }


        public Models.IdentityProvider Get(int id)
        {
            var item = this.SingleOrDefault(x => x.Id == id);
            return item != null ? item.ToDomainModel() : null;
        }
    }
}
