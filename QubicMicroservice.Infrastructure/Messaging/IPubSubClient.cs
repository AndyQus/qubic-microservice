namespace QubicMicroservice.Infrastructure.Messaging;

public interface IPubSubClient
{
    Task SubscribeAsync(string channelName);
}
