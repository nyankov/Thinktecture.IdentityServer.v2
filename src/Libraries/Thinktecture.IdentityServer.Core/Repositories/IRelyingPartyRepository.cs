/*
 * Copyright (c) Dominick Baier.  All rights reserved.
 * see license.txt
 */

using System.Collections.Generic;
using Thinktecture.IdentityServer.Models;

namespace Thinktecture.IdentityServer.Repositories
{
    public interface IRelyingPartyRepository
    {
        bool TryGet(string realm, out RelyingParty relyingParty);

        // management
        bool SupportsWriteAccess { get; }
        IEnumerable<RelyingParty> List(int pageIndex, int pageSize);
        RelyingParty GetByRealm(string realm);
        RelyingParty Get(string id);
        void Add(RelyingParty relyingParty);
        void Update(RelyingParty relyingParty);
        void Delete(string id);
    }
}
