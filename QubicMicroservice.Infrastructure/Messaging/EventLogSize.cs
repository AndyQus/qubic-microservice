namespace QubicMicroservice.Infrastructure.Messaging;

public enum EventLogSize
{
    QU_TRANSFER_LOG_SIZE = 72,
    ASSET_ISSUANCE_LOG_SIZE = 55,
    ASSET_OWNERSHIP_CHANGE_LOG_SIZE = 119,
    ASSET_POSSESSION_CHANGE_LOG_SIZE = 119,
    BURNING_LOG_SIZE = 40,
    DUST_BURNING_LOG_SIZE = 2621442,
    SPECTRUM_STATS_LOG_SIZE = 224
}
