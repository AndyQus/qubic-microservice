using StackExchange.Redis;
using QubicMicroservice.Domain.Interfaces;
using System.Text.Json;


namespace QubicMicroservice.Infrastructure.Messaging;

public class PubSubClient : IPubSubClient
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ITransactionRepository _transactionRepository;

    public PubSubClient(IConnectionMultiplexer redis, ITransactionRepository repository)
    {
        _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        _transactionRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task SubscribeAsync(string channelName)
    {
        var subscriber = _redis.GetSubscriber();
        await subscriber.SubscribeAsync(new RedisChannel(channelName, RedisChannel.PatternMode.Literal), async (channel, message) =>
        {
            Console.WriteLine($"Received message: {message}");

            if (message == RedisValue.Null) return;

            QubicMessage parsedMessage = JsonSerializer.Deserialize<QubicMessage>(message);

            // Process the event and write to the DB based on event type
            if (parsedMessage != null && parsedMessage.Header != null && !string.IsNullOrEmpty(parsedMessage.Header.EventId) && !string.IsNullOrEmpty(parsedMessage.Header.EventDigest))
            {
                await HandleEvent(parsedMessage);
            }
        });

        Console.WriteLine($"Subscribed to channel '{channelName}'.");
    }

    private async Task HandleEvent(QubicMessage message)
    {
        // Parse message and determine event type
        Console.WriteLine($"HandleEvent - Received message '{message.EventData}'.");

        byte[] decodedBytes = Convert.FromBase64String(message.EventData);

        // QuTransferEvent transferEvent = QuTransferEvent.FromBytes(decodedBytes);

        // TODO: Discrimine eventtype and call the appropriate repository methods to write to the database
        // if (message.Contains("eventTypeA"))
        // {
        //     Console.WriteLine($"Received eventTypeA '{message}'.");
        //     // Call repository methods for eventTypeA
        //     await _transactionRepository.GetAllAsync();

        // }
        // else if (message.Contains("eventTypeB"))
        // {
        //     // Call repository methods for eventTypeB
        //     Console.WriteLine($"Received eventTypeA '{message}'.");
        //     // Call repository methods for eventTypeA
        //     await _transactionRepository.GetAllAsync();
        // }
    }
}
