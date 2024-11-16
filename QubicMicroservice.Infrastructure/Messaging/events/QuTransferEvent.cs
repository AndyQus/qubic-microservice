using System;
using System.IO;
using System.Linq;

namespace QubicMicroservice.Infrastructure.Messaging;

public class QuTransferEvent
{
    public byte[] SourcePublicKey { get; private set; }
    public byte[] DestinationPublicKey { get; private set; }
    public ulong Amount { get; private set; }

    private QuTransferEvent(byte[] sourcePublicKey, byte[] destinationPublicKey, ulong amount)
    {
        SourcePublicKey = sourcePublicKey;
        DestinationPublicKey = destinationPublicKey;
        Amount = amount;
    }

    public static QuTransferEvent FromBytes(byte[] input)
    {
        if (input.Length != (int)EventLogSize.QU_TRANSFER_LOG_SIZE) // Ensure correct length (32 + 32 + 8 = 72 bytes)
            throw new ArgumentException("Input byte array has an invalid length for QuTransferEvent.");

        using (var memoryStream = new MemoryStream(input))
        using (var reader = new BinaryReader(memoryStream))
        {
            // Read 32 bytes for sourcePublicKey
            byte[] sourcePublicKey = reader.ReadBytes(32);

            // Read 32 bytes for destinationPublicKey
            byte[] destinationPublicKey = reader.ReadBytes(32);

            // Read 8 bytes for amount as little-endian ulong
            ulong amount = reader.ReadUInt64();

            // Ensure no remaining bytes in the input (optional check)
            if (memoryStream.Position != input.Length)
                throw new InvalidOperationException("Extra bytes found in input.");

            return new QuTransferEvent(sourcePublicKey, destinationPublicKey, amount);
        }
    }

    public override string ToString()
    {
        return $"QuTransferEvent{{sourcePublicKey={BitConverter.ToString(SourcePublicKey)}, " +
               $"destinationPublicKey={BitConverter.ToString(DestinationPublicKey)}, " +
               $"amount={Amount}}}";
    }
}
