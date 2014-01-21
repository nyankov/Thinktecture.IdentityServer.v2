using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    public static class MongoEntityRegistrator
    {
        public static void RegisterEntity()
        {
            BsonClassMap.RegisterClassMap<Entity<string>>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance));
            });
        }
    }
}
