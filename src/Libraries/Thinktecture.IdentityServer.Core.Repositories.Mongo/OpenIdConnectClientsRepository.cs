using System;
using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class OpenIdConnectClientsRepository : MongoRepository<OpenIdConnectClientEntity>, IOpenIdConnectClientsRepository
    {
        public OpenIdConnectClientsRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }
        public bool ValidateClient(string clientId, string clientSecret)
        {
            Models.OpenIdConnectClient client;
            return ValidateClient(clientId, clientSecret, out client);

        }

        public bool ValidateClient(string clientId, string clientSecret, out Models.OpenIdConnectClient client)
        {
            var record = this.FirstOrDefault(cl=>cl.ClientId==clientId);
            if (record != null)
            {
                if (Helper.CryptoHelper.VerifyHashedPassword(record.ClientSecret,clientSecret))
                {
                    client = record.ToDomainModel();
                    return true;
                }
            }

            client = null;
            return false;
        }

        public IEnumerable<Models.OpenIdConnectClient> GetAll()
        {
            return this.ToArray().Select(x => x.ToDomainModel()).ToArray();
        }

        public Models.OpenIdConnectClient Get(string clientId)
        {
            var item = this.SingleOrDefault(x => x.ClientId == clientId);
            return item != null ? item.ToDomainModel() : null;
        }

        public void Delete(string clientId)
        {
            var item = this.FirstOrDefault(c=>c.ClientId==clientId);
            if (item == null) return;
            Delete(item);
        }

        public void Update(Models.OpenIdConnectClient model)
        {
            if (model == null) throw new ArgumentNullException("model");
            var item = this.FirstOrDefault(c=>c.ClientId==model.ClientId);
            if (item == null) return;
            model.UpdateEntity(item);
            Update(item);
        }

        public void Create(Models.OpenIdConnectClient model)
        {
            if (model == null) throw new ArgumentNullException("model");
            var item = new OpenIdConnectClientEntity();
            model.UpdateEntity(item);
            Add(item);
        }
    }
}
