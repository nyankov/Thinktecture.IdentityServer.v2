namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }

    public interface IEntity : IEntity<uint>
    {
        
    }
}
