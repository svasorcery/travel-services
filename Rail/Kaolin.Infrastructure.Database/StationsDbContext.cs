using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kaolin.Infrastructure.Database
{
    [BsonIgnoreExtraElements]
    public class StationDoc
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("code")]
        public int Express { get; set; }

        [BsonElement("esr")]
        public int? Esr { get; set; }

        [BsonElement("loc")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [BsonElement("c")]
        public string Country { get; set; }

        [BsonElement("r")]
        public string Region { get; set; }

        [BsonElement("rw")]
        public string Railway { get; set; }

        [BsonElement("lat")]
        public double? Latitude { get; set; }

        [BsonElement("lon")]
        public double? Longitude { get; set; }
    }


    public class StationsDbContext : MongoDbContextBase
    {
        private IMongoCollection<StationDoc> _stations;

        public StationsDbContext(IOptions<Config> optionsAccessor)
            : base(optionsAccessor)
        {
            _stations = Database.GetCollection<StationDoc>(
                optionsAccessor.Value.StationsCollection);
        }

        public Task<List<StationDoc>> GetAllAsync()
        {
            return _stations.Find(_ => true).ToListAsync();
        }

        public Task<List<StationDoc>> SearchAsync(string term, int? limit = null)
        {
            var q = term.ToLower().Trim();
            var l = limit ?? 10;
            return _stations.Find(x => x.Name.ToLower().StartsWith(q))
                .Limit(l)
                .ToListAsync();
        }

        public Task<StationDoc> FindByCodeAsync(int code)
        {
            return _stations.Find(x => x.Express == code).FirstOrDefaultAsync();
        }

        public Task<StationDoc> FindByEsrAsync(int esr)
        {
            return _stations.Find(x => x.Esr == esr).FirstOrDefaultAsync();
        }

        public Task<StationDoc> FindByNameAsync(string name)
        {
            return _stations.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public Task ReplaceOneAsync(int code, StationDoc doc)
        {
            doc.Express = code;
            return _stations.ReplaceOneAsync(x => x.Express == code, doc, new UpdateOptions { IsUpsert = true });
        }

        public Task StoreOneAsync(StationDoc doc)
        {
            return _stations.InsertOneAsync(doc);
        }

        public Task StoreManyAsync(IEnumerable<StationDoc> docs)
        {
            return _stations.InsertManyAsync(docs);
        }

        public Task DeleteOneAsync(string id)
        {
            return _stations.DeleteOneAsync(x => x.Id == id);
        }
    }
}
