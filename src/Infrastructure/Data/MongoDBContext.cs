using MongoDB.Driver;
using Microsoft.Extensions.Options;
using QubicMicroservice.Domain.Entities;

namespace QubicMicroservice.Infrastructure.Data;

public class MongoDBContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDBSettings _settings;

    public MongoDBContext(IOptions<MongoDBSettings> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Transaction> Transactions =>
        _database.GetCollection<Transaction>(_settings.TransactionsCollectionName);
}
