using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    public class MongoRepository<T, TKey> : IRepository<T, TKey>
        where T : IEntity<TKey>
    {
        protected internal MongoCollection<T> Collection;

        public MongoRepository(IMongoConnectionString connectionString)
            : this(connectionString.Value)
        {
        }

        public MongoRepository(string connectionString)
        {
            Collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }

        public MongoRepository(string connectionString, string collectionName)
        {
            Collection = Util<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public MongoRepository(MongoUrl url)
        {
            Collection = Util<TKey>.GetCollectionFromUrl<T>(url);
        }

        public MongoRepository(MongoUrl url, string collectionName)
        {
            Collection = Util<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }

        public virtual T GetById(TKey id)
        {
            return Collection.FindOneByIdAs<T>(typeof(T).IsSubclassOf(typeof(Entity<>)) 
                        ? new ObjectId(id as string) 
                        : BsonValue.Create(id));
        }

        public virtual T Add(T entity)
        {
            Collection.Insert<T>(entity);

            return entity;
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            Collection.InsertBatch<T>(entities);
        }

        public virtual T Update(T entity)
        {
            Collection.Save<T>(entity);
            return entity;
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Collection.Save<T>(entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            Collection.Remove(typeof (T).IsSubclassOf(typeof (Entity<>))
                ? Query.EQ("_id", new ObjectId(id as string))
                : Query.EQ("_id", BsonValue.Create(id)));
        }

        public virtual void Delete(ObjectId id)
        {
            Collection.Remove(Query.EQ("_id", id));
        }

        public virtual void Delete(T entity)
        {
            Delete(entity.Id);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (var entity in Collection.AsQueryable().Where(predicate))
            {
                Delete(entity.Id);
            }
        }

        public virtual void DeleteAll()
        {
            Collection.RemoveAll();
        }

        public virtual long Count()
        {
            return Collection.Count();
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Collection.AsQueryable().Any(predicate);
        }
        

        #region IQueryable<T>
        
        public virtual IEnumerator<T> GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        public virtual Type ElementType
        {
            get { return Collection.AsQueryable().ElementType; }
        }

        public virtual Expression Expression
        {
            get { return Collection.AsQueryable().Expression; }
        }

        public virtual IQueryProvider Provider
        {
            get { return Collection.AsQueryable().Provider; }
        }
        #endregion
    }

    public class MongoRepository<T> : MongoRepository<T, string> where T : IEntity<string>
    {
        public MongoRepository(IMongoConnectionString connectionString)
            : base(connectionString)
        { }

        public MongoRepository(MongoUrl url)
            : base(url) { }

        public MongoRepository(MongoUrl url, string collectionName)
            : base(url, collectionName) { }

        public MongoRepository(string connectionString)
            : base(connectionString) { }

        public MongoRepository(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}
