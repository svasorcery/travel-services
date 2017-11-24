using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kaolin.Infrastructure.Database
{
    public class CountryDoc
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cid")]
        public int CountryId { get; set; }

        [BsonElement("rzd")]
        public int? CountryIdRzd { get; set; }

        [BsonElement("code")]
        public string Alpha2 { get; set; }

        [BsonElement("ru")]
        public string NameRu { get; set; }

        [BsonElement("en")]
        public string NameEn { get; set; }
    }


    public class CountriesDbContext : MongoDbContextBase
    {
        private IMongoCollection<CountryDoc> _countries;

        public CountriesDbContext(IOptions<Config> optionsAccessor)
            : base(optionsAccessor)
        {
            _countries = Database.GetCollection<CountryDoc>(
                optionsAccessor.Value.CountriesCollection);
        }

        public Task<List<CountryDoc>> GetAllAsync()
        {
            return _countries.Find(_ => true).ToListAsync();
        }

        public Task<List<CountryDoc>> SearchAsync(string term)
        {
            var q = term.ToLower().Trim();
            return _countries.Find(x => x.NameRu.ToLower().Contains(q)).ToListAsync();
        }

        public Task<CountryDoc> FindByIdAsync(int id)
        {
            return _countries.Find(x => x.CountryId == id).FirstOrDefaultAsync();
        }

        public Task<CountryDoc> FindByRzdIdAsync(int rzdId)
        {
            return _countries.Find(x => x.CountryIdRzd == rzdId).FirstOrDefaultAsync();
        }

        public Task<CountryDoc> FindByNameAsync(string name)
        {
            return _countries.Find(x => x.NameRu == name).FirstOrDefaultAsync();
        }

        public Task ReplaceOneAsync(int id, CountryDoc doc)
        {
            doc.CountryId = id;
            return _countries.ReplaceOneAsync(x => x.CountryId == id, doc, new UpdateOptions { IsUpsert = true });
        }

        public Task StoreOneAsync(CountryDoc doc)
        {
            return _countries.InsertOneAsync(doc);
        }

        public Task StoreOManyAsync(IEnumerable<CountryDoc> docs)
        {
            return _countries.InsertManyAsync(docs);
        }
    }
}
