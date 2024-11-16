using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace QubicMicroservice.Infrastructure.Messaging;
public class PubSubBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly List<string> _channels;

    public PubSubBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _channels = new List<string>
        {
            EventType.QU_TRANSFER.GetEventChannel(),
            // EventType.ASSET_ISSUANCE.GetEventChannel(),
            // EventType.ASSET_OWNERSHIP_CHANGE.GetEventChannel(),
            // EventType.ASSET_OWNERSHIP_CHANGE.GetEventChannel()
        };
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var pubSubClient = scope.ServiceProvider.GetRequiredService<IPubSubClient>();
            foreach (var channel in _channels)
            {
                await pubSubClient.SubscribeAsync(channel);
                Console.WriteLine($"Subscribed to channel '{channel}' in background service.");
            }
        }

        // Keep the service alive
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
