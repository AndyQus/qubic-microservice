using System;
using Xunit;
using li.qubic.lib;
using QubicMicroservice.Infrastructure.Messaging;

namespace QubicMicroservice.Tests
{
    public class QuTransferEventTests
    {

        [Fact]
        public void FromBytes_ValidInput_ShouldReturnCorrectEvent()
        {
            // Arrange
            var qubicHelper = new QubicHelper();

            string base64Input = "jo0AwIVOmnDHyw6JCcNWOuWlngm9zxDGrtcyA3WjioYBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEBCDwAAAAAA";

            // Decode Base64 to bytes
            byte[] bytes = Convert.FromBase64String(base64Input);

            // Act: Parse the bytes to create a QuTransferEvent
            QuTransferEvent result = QuTransferEvent.FromBytes(bytes);

            // Expected values
            ulong expectedAmount = 1_000_000;

            // Print the event for debugging purposes
            Console.WriteLine("[TEST LOG] - ",result);

            // Assert: Validate the parsed values
            Assert.NotNull(result);
            Assert.Equal(qubicHelper.GetPublicKeyFromIdentity("KEFDZTYLFERAHDVLNQORDHFQIBSBZCWSZXZFFANOTFAHWMOVGTRQJPXDVCWG"), result.SourcePublicKey);
            Assert.Equal(qubicHelper.GetPublicKeyFromIdentity("BAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARMID"), result.DestinationPublicKey);
            Assert.Equal(expectedAmount, result.Amount);
        }
    }
}
