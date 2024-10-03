using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Traccia3.Models.DB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<Attivita> Attivita => _database.GetCollection<Attivita>("Attivita");

    }
}
