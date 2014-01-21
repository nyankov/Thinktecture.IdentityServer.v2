/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.Data;
using CodeToken = Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.CodeToken;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class CodeTokenRepository : MongoRepository<CodeToken, int>, ICodeTokenRepository
    {
        public CodeTokenRepository()
            : base(Util<int>.GetDefaultConnectionString())
        {
            
        }    

        public string AddCode(CodeTokenType type, int clientId, string userName, string scope)
        {
            var code = Guid.NewGuid().ToString("N");

            var refreshToken = new CodeToken
            {
                Type = (int) type,
                Code = code,
                ClientId = clientId,
                Scope = scope,
                UserName = userName,
                TimeStamp = DateTime.UtcNow
            };

            Add(refreshToken);
            return code;
        }

        public bool TryGetCode(string code, out Models.CodeToken token)
        {
            token = null;

            var entity = this.FirstOrDefault(t => t.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (entity == null) return false;

            token = entity.ToDomainModel();
            return true;
        }

        public void DeleteCode(string code)
        {
            var item = this.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (item == null) return;
            Delete(item);
        }

        public IEnumerable<Models.CodeToken> Search(int? clientId, string username, string scope, CodeTokenType type)
        {
            var query = this.Where(t => t.Type == (int) type);

            if (clientId != null)
            {
                query = query.Where(t => t.ClientId == clientId.Value);
            }

            if (!String.IsNullOrWhiteSpace(username))
            {
                query = query.Where(t => t.UserName.Contains(username));
            }

            if (!String.IsNullOrWhiteSpace(scope))
            {
                query = query.Where(t => t.Scope.Contains(scope));
            }

            var results = query.ToArray().Select(x => x.ToDomainModel());
            return results;
        }
    }
}
