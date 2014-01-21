using System;
using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class ClientsRepository : MongoRepository<Client, int>, IClientsRepository
    {
        public ClientsRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
        
        }

        public bool ValidateClient(string clientId, string clientSecret)
        {
                var record = this.SingleOrDefault(c => c.ClientId.Equals(clientId, StringComparison.Ordinal));
                return record != null && Helper.CryptoHelper.VerifyHashedPassword(record.ClientSecret, clientSecret);
        }

        public bool TryGetClient(string clientId, out Models.Client client)
        {
                var record = this.SingleOrDefault(c => c.ClientId.Equals(clientId, StringComparison.Ordinal));

                if (record != null)
                {
                    client = record.ToDomainModel();
                    return true;
                }

                client = null;
                return false;
        }

        public bool ValidateAndGetClient(string clientId, string clientSecret, out Models.Client client)
        {
            var record = this.SingleOrDefault(c => c.ClientId.Equals(clientId, StringComparison.Ordinal));
            if (record != null)
            {
                if (Helper.CryptoHelper.VerifyHashedPassword(record.ClientSecret, clientSecret))
                {
                    client = record.ToDomainModel();
                    return true;
                }
            }

            client = null;
            return false;
        }

        public IEnumerable<Models.Client> GetAll()
        {
            return this.ToArray().Select(x => x.ToDomainModel()).ToArray();
        }


        public override void Delete(int id)
        {
            var item = this.SingleOrDefault(x => x.Id == id);
            if (item == null) return;
            base.Delete(item);
        }

        public void Update(Models.Client model)
        {
            if (model == null) throw new ArgumentException("model");

            var item = this.Single(x => x.Id == model.ID);
            model.UpdateEntity(item);
            base.Update(item);
        }

        public void Create(Models.Client model)
        {
            if (model == null) throw new ArgumentException("model");

            var item = new Client();
            model.UpdateEntity(item);
            Add(item);
            model.ID = item.Id;
        }


        public Models.Client Get(int id)
        {
            var item = this.SingleOrDefault(x => x.Id == id);
            return item != null ? item.ToDomainModel() : null;
        }
    }
}
