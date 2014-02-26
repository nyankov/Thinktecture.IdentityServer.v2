using System;

namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    [Serializable]
    public abstract class Entity<TId> : IEntity<TId> where TId : IComparable
    {
        public virtual TId Id { get; set; }

        public override bool Equals(object entity)
        {
            if (entity == null || !(entity is Entity<TId>))
                return false;

            return this == (Entity<TId>)entity;
        }

        public static bool operator ==(Entity<TId> entity1, Entity<TId> entity2)
        {
            var obj1 = entity1 as object;
            var obj2 = entity2 as object;

            if ((obj1 == null) && (obj2 == null))
                return true;

            if ((obj1 == null) || (obj2 == null))
                return false;

            if (entity1.GetType() != entity2.GetType())
                return false;

            return entity1.Id.CompareTo(entity2.Id) == 0;
        }

        public static bool operator !=(Entity<TId> entity1, Entity<TId> entity2)
        {
            return (!(entity1 == entity2));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }


    public abstract class Entity : Entity<string>
    {

    }
}
