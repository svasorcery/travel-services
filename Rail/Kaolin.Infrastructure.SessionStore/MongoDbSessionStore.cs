using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Kaolin.Infrastructure.SessionStore
{
    internal class MongoDbSessionStore : ISessionStore
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public DateTime Ttl { get; set; }
        
        public Dictionary<string, BsonDocument> Data { get; protected set; }

        public MongoDbSessionStore(TimeSpan ttl) : this(
            ObjectId.GenerateNewId().ToString(), 
            DateTime.UtcNow + ttl)
        {
        }

        public MongoDbSessionStore(string id, DateTime ttl)
        {
            Id = id;
            Ttl = ttl;
            Data = new Dictionary<string, BsonDocument>();
        }


        public void Store<T>(string key, T value)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = value.ToBsonDocument();
            }
            else
            {
                Data.Add(key, value.ToBsonDocument());
            }
        }

        public T Retrieve<T>(string key) 
            => Data.ContainsKey(key) ? BsonSerializer.Deserialize<T>(Data[key]) : default(T);
    }
}
