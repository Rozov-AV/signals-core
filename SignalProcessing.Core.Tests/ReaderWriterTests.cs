using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class ReaderWriterTests
    {
        [Fact]
        public void WriteThanRead()
        {
            // Arrange
            float[] data = { 0.5f, -1.25f, 3, 0.1f };
            float frequency = 250;
            SingleChannelSignal signal = new SingleChannelSignal(data, frequency);

            // Act
            string fileName = "test.csv";
            SingleChannelSignalWriter.Write(signal, fileName);
            SingleChannelSignal actual = SingleChannelSignalReader.Read(fileName);

            // Assert
            Assert.Equal(frequency, actual.Frequency);
            Assert.Equal(data.Length, actual.Data.Length);
            AssertSignal.Equal(data, actual.Data);
        }
    }
}
