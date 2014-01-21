using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Thinktecture.IdentityServer.Repositories.Mongo.Data.CollectionManager
{
    public class MongoCollectionManager<T, TKey> : IMongoCollectionManager<T, TKey>
        where T : IEntity<TKey>
    {
        private readonly MongoCollection<T> _collection;

        public MongoCollectionManager(IMongoConnectionString connectionString)
            : this(connectionString.Value)
        {
        }

        public MongoCollectionManager(string connectionString)
        {
            _collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }

        public MongoCollectionManager(string connectionString, string collectionName)
        {
            _collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public virtual bool Exists
        {
            get { return _collection.Exists(); }
        }

        public virtual string Name
        {
            get { return _collection.Name; }
        }

        public virtual void Drop()
        {
            _collection.Drop();
        }

        public virtual bool IsCapped()
        {
            return _collection.IsCapped();
        }

        public virtual void DropIndex(string keyname)
        {
            DropIndexes(new[] { keyname });
        }

        public virtual void DropIndexes(IEnumerable<string> keynames)
        {
            _collection.DropIndex(keynames.ToArray());
        }

        public virtual void DropAllIndexes()
        {
            _collection.DropAllIndexes();
        }

        public virtual void EnsureIndex(string keyname)
        {
            EnsureIndexes(new[] { keyname });
        }

        public virtual void EnsureIndex(string keyname, bool descending, bool unique, bool sparse)
        {
            EnsureIndexes(new[] { keyname }, descending, unique, sparse);
        }

        public virtual void EnsureIndexes(IEnumerable<string> keynames)
        {
            EnsureIndexes(keynames, false, false, false);
        }

        public virtual void EnsureIndexes(IEnumerable<string> keynames, bool descending, bool unique, bool sparse)
        {
            var ixk = new IndexKeysBuilder();
            if (descending)
            {
                ixk.Descending(keynames.ToArray());
            }
            else
            {
                ixk.Ascending(keynames.ToArray());
            }

            EnsureIndexes(
                ixk,
                new IndexOptionsBuilder().SetUnique(unique).SetSparse(sparse));
        }

        public virtual void EnsureIndexes(IMongoIndexKeys keys, IMongoIndexOptions options)
        {
            _collection.EnsureIndex(keys, options);
        }

        public virtual bool IndexExists(string keyname)
        {
            return IndexesExists(new[] { keyname });
        }

        public virtual bool IndexesExists(IEnumerable<string> keynames)
        {
            return _collection.IndexExists(keynames.ToArray());
        }

        public virtual void ReIndex()
        {
            _collection.ReIndex();
        }

        public virtual long GetTotalDataSize()
        {
            return _collection.GetTotalDataSize();
        }

        public virtual long GetTotalStorageSize()
        {
            return _collection.GetTotalStorageSize();
        }

        public virtual ValidateCollectionResult Validate()
        {
            return _collection.Validate();
        }

        public virtual CollectionStatsResult GetStats()
        {
            return _collection.GetStats();
        }

        public virtual GetIndexesResult GetIndexes()
        {
            return _collection.GetIndexes();
        }
    }

    public class MongoCollectionManager<T> : MongoCollectionManager<T, string>, IMongoCollectionManager<T>
        where T : IEntity<string>
    {
        public MongoCollectionManager(IMongoConnectionString connectionString)
            :base(connectionString)
        { }

        public MongoCollectionManager(string connectionString)
            : base(connectionString) { }

        public MongoCollectionManager(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}
