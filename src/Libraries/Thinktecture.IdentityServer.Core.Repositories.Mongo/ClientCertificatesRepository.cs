using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class ClientCertificatesRepository : MongoRepository<ClientCertificates, int>,IClientCertificatesRepository
    {
        public ClientCertificatesRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }

        #region Runtime

        public bool TryGetUserNameFromThumbprint(X509Certificate2 certificate, out string userName)
        {
            userName = this.Where(mapping => mapping.Thumbprint.Equals(certificate.Thumbprint, StringComparison.OrdinalIgnoreCase))
                           .Select(mapping => mapping.UserName).FirstOrDefault();

            return (userName != null);

        }

        #endregion

        #region Management
        public bool SupportsWriteAccess
        {
            get { return true; }
        }

        public IEnumerable<string> List(int pageIndex, int pageSize)
        {
                var users =this.OrderBy(user => user.UserName)
                               .Select(user => user.UserName)
                               .Distinct();

                if (pageIndex != -1 && pageSize != -1)
                {
                    users = users.Skip(pageIndex * pageSize).Take(pageSize);
                }

                return users.ToList();
        }

        public IEnumerable<ClientCertificate> GetClientCertificatesForUser(string userName)
        {
            var certs = this.Where(record => record.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            return certs.ToList().ToDomainModel();
        }

        public void Add(ClientCertificate certificate)
        {
            var record =
                this.SingleOrDefault(
                    entry => entry.UserName.Equals(certificate.UserName, StringComparison.OrdinalIgnoreCase) &&
                             entry.Thumbprint.Equals(certificate.Thumbprint, StringComparison.OrdinalIgnoreCase));
            if (record == null)
            {
                record = new ClientCertificates
                {
                    UserName = certificate.UserName,
                    Thumbprint = certificate.Thumbprint,
                    Description = certificate.Description
                };
                base.Add(record);
            }
            else
            {
                record.Description = certificate.Description;
                base.Update(record);
            }
        }

        public void Delete(ClientCertificate certificate)
        {
            var record =
                this.SingleOrDefault(
                    entry => entry.UserName.Equals(certificate.UserName, StringComparison.OrdinalIgnoreCase) &&
                             entry.Thumbprint.Equals(certificate.Thumbprint, StringComparison.OrdinalIgnoreCase));
            if (record == null) return;

            base.Delete(record);
        }
        #endregion
    }
}
