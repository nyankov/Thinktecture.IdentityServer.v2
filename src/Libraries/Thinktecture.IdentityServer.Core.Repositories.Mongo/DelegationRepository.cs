using System;
using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class DelegationRepository : MongoRepository<Delegation, int>, IDelegationRepository
    {
        public DelegationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }

        #region Runtime

        public bool IsDelegationAllowed(string userName, string realm)
        {
            var record =
                this.FirstOrDefault(entry => entry.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                                             entry.Realm.Equals(realm, StringComparison.OrdinalIgnoreCase));

            return (record != null);
        }

        #endregion

        #region Management

        public bool SupportsWriteAccess
        {
            get { return true; }
        }

        public IEnumerable<string> GetAllUsers(int pageIndex, int pageSize)
        {
            var users = this.OrderBy(user => user.UserName).Select(user => user.UserName);

            if (pageIndex != -1 && pageSize != -1)
            {
                users = users.Skip(pageIndex*pageSize).Take(pageSize);
            }

            return users.ToList();
        }

        public IEnumerable<DelegationSetting> GetDelegationSettingsForUser(string userName)
        {
            var settings = this.Where(record => record.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            return settings.ToList().ToDomainModel();
        }

        public void Add(DelegationSetting setting)
        {
            var entity = new Delegation
            {
                UserName = setting.UserName,
                Realm = setting.Realm.AbsoluteUri,
                Description = setting.Description
            };

            Add(entity);
        }

        public void Delete(DelegationSetting setting)
        {
            var record =
                this.Single(
                    entry => entry.UserName.Equals(setting.UserName, StringComparison.OrdinalIgnoreCase) &&
                             entry.Realm.Equals(setting.Realm.AbsoluteUri, StringComparison.OrdinalIgnoreCase));

            Delete(record);
        }

        #endregion
    }
}
