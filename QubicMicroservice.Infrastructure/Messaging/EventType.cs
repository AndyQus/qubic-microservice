namespace QubicMicroservice.Infrastructure.Messaging;

public enum EventType
{
    QU_TRANSFER = 0,
    ASSET_ISSUANCE = 1,
    ASSET_OWNERSHIP_CHANGE = 2,
    ASSET_POSSESSION_CHANGE = 3,
    BURNING = 4,
    DUST_BURNING = 5,
    SPECTRUM_STATS = 6
}

public static class EventTypeExtensions
{
    public static string GetEventChannel(this EventType eventType)
    {
        return eventType switch
        {
            EventType.QU_TRANSFER => "eventsbytype0",
            EventType.ASSET_ISSUANCE => "eventsbytype1",
            EventType.ASSET_OWNERSHIP_CHANGE => "eventsbytype2",
            EventType.ASSET_POSSESSION_CHANGE => "eventsbytype3",
            EventType.BURNING => "eventsbytype4",
            EventType.DUST_BURNING => "eventsbytype5",
            EventType.SPECTRUM_STATS => "eventsbytype6",
            _ => throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null)
        };
    }
}

