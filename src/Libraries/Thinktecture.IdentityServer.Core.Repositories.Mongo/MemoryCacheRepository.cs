using System;
using System.Runtime.Caching;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class MemoryCacheRepository : ICacheRepository
    {
        static readonly MemoryCache Cache = new MemoryCache("Thinktecture.IdentityServer.Caching");
        
        public void Put(string name, object value, int ttl)
        {
            Tracing.Verbose(String.Format("Adding {0} to cache", name));
            Cache.Add(name, value, DateTimeOffset.Now.AddHours(ttl));
        }

        public object Get(string name)
        {
            var item = Cache.Get(name);
            Tracing.Verbose(String.Format("Fetching {0} from cache: {1}", name, item == null ? "miss" : "hit"));

            return item;
        }

        public void Invalidate(string name)
        {
            Tracing.Verbose(String.Format("Invalidating {0} in cache", name));
            Cache.Remove(name);
        }
    }
}
