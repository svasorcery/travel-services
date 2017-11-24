using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Kaolin.Infrastructure.SessionStore
{
    internal class MongoDbSessionProvider : ISessionProvider
    {
        private readonly IMongoCollection<MongoDbSessionStore> _collection;
        private readonly ILogger<MongoDbSessionProvider> _logger;

        public MongoDbSessionProvider(IOptions<Config> configAccessor, ILogger<MongoDbSessionProvider> logger)
        {
            var options = configAccessor.Value;

            _collection = new MongoClient(options.ConnectionString)
                .GetDatabase(options.Database)
                .GetCollection<MongoDbSessionStore>(options.Collection);

            _logger = logger;
        }


        public ISessionStore Create(TimeSpan ttl)
        {
            var id = ObjectId.GenerateNewId().ToString();
            var tt = DateTime.UtcNow + ttl;

            _logger.LogDebug("New session id:{sessionId} ttl:{ttl}", id, tt);

            return new MongoDbSessionStore(id, tt);
        }

        public Task SaveAsync(ISessionStore session)
        {
            var doc = (MongoDbSessionStore)session;

            _logger.LogDebug("Save session id:{sessionId} ttl:{ttl} keys:{data}", doc.Id, doc.Ttl, doc.Data.Keys);

            return _collection.ReplaceOneAsync(x => x.Id == doc.Id, (MongoDbSessionStore)session, new UpdateOptions { IsUpsert = true });
        }

        public async Task<ISessionStore> LoadAsync(string id)
        {
            _logger.LogDebug("Load session id:{sessionId}", id);

            var doc = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (doc == null)
            {
                _logger.LogInformation("Session not found id:{sessionId}", id);
            }
            else
            {
                _logger.LogDebug("Session loaded id:{sessionId} ttl:{ttl} keys:{data}", doc.Id, doc.Ttl, doc.Data.Keys);
            }

            return doc;
        }
    }
}
