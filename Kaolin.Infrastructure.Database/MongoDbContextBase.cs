using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kaolin.Infrastructure.Database
{
    public class MongoDbContextBase
    {
        private readonly IMongoDatabase _database;

        public MongoDbContextBase(IOptions<Config> settings)
        {
            if (settings.Value == null)
                throw new ArgumentNullException(nameof(settings), $"{nameof(Config)} cannot be null.");

            if (settings.Value.ConnectionString == null)
                throw new ArgumentNullException(nameof(settings), $"{nameof(Config)}.ConnectionString cannot be null.");

            if (settings.Value.Database == null)
                throw new ArgumentNullException(nameof(settings), $"{nameof(Config)}.DatabaseName cannot be null.");

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        protected IMongoDatabase Database { get { return _database; } }
    }
}
