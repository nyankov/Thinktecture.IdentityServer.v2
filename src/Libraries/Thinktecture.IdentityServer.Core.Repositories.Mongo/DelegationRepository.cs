using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class DelegationRepository : MongoRepository<Delegation>, IDelegationRepository
    {
        public DelegationRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {

        }

        #region Runtime

        public bool IsDelegationAllowed(string userName, string realm)
        {
            var regex = new Regex("^" + userName + "$", RegexOptions.IgnoreCase);
            var regex1 = new Regex("^" + realm + "$", RegexOptions.IgnoreCase);
            var record = this.FirstOrDefault(entry => regex.IsMatch(entry.UserName) && regex1.IsMatch(entry.Realm));
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
            var regex = new Regex("^" + userName + "$", RegexOptions.IgnoreCase);
            var settings = this.Where(record => regex.IsMatch(record.UserName));

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
            var regex = new Regex("^" + setting.UserName + "$", RegexOptions.IgnoreCase);
            var regex1 = new Regex("^" + setting.Realm + "$", RegexOptions.IgnoreCase);
            var record = this.FirstOrDefault(entry => regex.IsMatch(entry.UserName) && regex1.IsMatch(entry.Realm));
            if (record == null) return;
            Delete(record);
        }

        #endregion
    }
}
