using System;
using System.Configuration;
using MongoDB.Driver;

namespace Thinktecture.IdentityServer.Repositories.Mongo.Data
{
    internal static class Util<TU>
    {
        private const string DefaultConnectionstringName = "MongoServerSettings";
        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DefaultConnectionstringName].ConnectionString;
        }

        private static MongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            var client = new MongoClient(url);
            var server = client.GetServer();
            return server.GetDatabase(url.DatabaseName);
        }

        public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString)
            where T : IEntity<TU>
        {
            return GetCollectionFromConnectionString<T>(connectionString, GetCollectionName<T>());
        }

        public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString, string collectionName)
            where T : IEntity<TU>
        {
            return GetDatabaseFromUrl(new MongoUrl(connectionString)).GetCollection<T>(collectionName);
        }

        public static MongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url)
            where T : IEntity<TU>
        {
            return GetCollectionFromUrl<T>(url, GetCollectionName<T>());
        }

        public static MongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url, string collectionName)
            where T : IEntity<TU>
        {
            return GetDatabaseFromUrl(url).GetCollection<T>(collectionName);
        }

        private static string GetCollectionName<T>() where T : IEntity<TU>
        {
            string collectionName;
            var baseType = typeof(T).BaseType;
            if (baseType != null && baseType == typeof(object))
            {
                collectionName = GetCollectioNameFromInterface<T>();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        private static string GetCollectioNameFromInterface<T>()
        {
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            var collectionname = att != null ? ((CollectionName)att).Name
                                             : Pluralize(typeof(T).Name.ToLower());

            return collectionname;
        }

        private static string GetCollectionNameFromType(Type entitytype)
        {
            string collectionname;

            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));
            if (att != null)
            {
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                while (entitytype.BaseType != null && !(entitytype.BaseType == typeof(Entity<int>)))
                {
                    entitytype = entitytype.BaseType;
                }

                collectionname = Pluralize(entitytype.Name.ToLower());
            }

            return collectionname;
        }

        private static string Pluralize(string word)
        {
            var lastCharacter = String.Empty;
            var lastTwoCharacters = String.Empty;

            if (word.Length >= 2) lastCharacter = word.Substring(word.Length - 1);
            if (word.Length >= 3) lastTwoCharacters = word.Substring(word.Length - 2);

            // Look for ss, such as compass
            var containsSs = (lastTwoCharacters.ToLower() == "ss");
            // if ss, then add es: Example compass -> compasses
            if (containsSs)
            {
                word += "es";
                return word;
            }

            // Look for s, such as books
            var containsS = (lastCharacter.ToLower() == "s");
            // do nothing
            if (containsS) return word;

            // Look for y, such as inventory
            var containsY = (lastCharacter.ToLower() == "y");
            // change to ies: Example inventory -> inventories
            if (!containsY) return word + "s";

            var modified = word.Substring(0, word.Length - 1);
            modified += "ies";
            return modified;
        }
    }
}
