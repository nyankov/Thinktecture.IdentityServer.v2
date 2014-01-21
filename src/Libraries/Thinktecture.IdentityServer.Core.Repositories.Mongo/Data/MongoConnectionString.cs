namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    public class MongoConnectionString: IMongoConnectionString
    {
        public MongoConnectionString()
        {
            Value = Util<string>.GetDefaultConnectionString();
        }

        public string Value { get; private set; }
    }
}