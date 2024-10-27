using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QubicMicroservice.Domain.Entities;

public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("transactionType")]
    public required string TransactionType { get; set; }

    [BsonElement("amount")]
    public decimal Amount { get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("description")]
    public required string Description { get; set; }
}
