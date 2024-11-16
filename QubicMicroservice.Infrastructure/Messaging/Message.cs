using System.Text.Json.Serialization;

namespace QubicMicroservice.Infrastructure.Messaging;
public class QubicMessage
{
    [JsonPropertyName("header")]
    public required Header Header { get; set; }

    [JsonPropertyName("eventSize")]
    public int EventSize { get; set; }

    [JsonPropertyName("eventData")]
    public required string EventData { get; set; }
}

public class Header
{
    [JsonPropertyName("epoch")]
    public int Epoch { get; set; }

    [JsonPropertyName("tick")]
    public int Tick { get; set; }

    [JsonPropertyName("tmp")]
    public int Tmp { get; set; }

    [JsonPropertyName("eventId")]
    public required string EventId { get; set; }

    [JsonPropertyName("eventDigest")]
    public required string EventDigest { get; set; }
}
