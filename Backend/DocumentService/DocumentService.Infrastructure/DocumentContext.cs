using DocumentService.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DocumentService.Infrastructure
{
    public class DocumentContext
    {
        private readonly IMongoDatabase _database;

        public DocumentContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Document> Documents => _database.GetCollection<Document>("Documents");
    }
}