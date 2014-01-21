using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    public interface IRepository<T, in TKey>: IQueryable<T> where T : IEntity<TKey>
    {
        T GetById(TKey id);
        T Add(T entity);
        void Add(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(TKey id);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void DeleteAll();
        long Count();
        bool Exists(Expression<Func<T, bool>> predicate);
    }

    public interface IRepository<T> : IRepository<T, uint>
        where T : IEntity
    {
        
    }
}
