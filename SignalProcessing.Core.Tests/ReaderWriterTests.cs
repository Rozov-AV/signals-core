using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class ReaderWriterTests : IDisposable
    {
        private readonly string _filePath;

        public ReaderWriterTests()
        {
            // выполняется перед каждым тестом
            _filePath = Path.GetTempFileName();
        }

        [Fact]
        public void WriteThanRead()
        {
            // Arrange
            float[] data = { 0.5f, -1.25f, 3, 0.1f };
            float frequency = 250;
            SingleChannelSignal signal = new SingleChannelSignal(data, frequency);

            // Act
            SingleChannelSignalWriter.Write(signal, _filePath);
            SingleChannelSignal actual = SingleChannelSignalReader.Read(_filePath);

            // Assert
            Assert.Equal(frequency, actual.Frequency);
            Assert.Equal(data.Length, actual.Data.Length);
            AssertSignal.Equal(data, actual.Data);
        }

        [Fact]
        public void Read_EmptySignal()
        {
            // Arrange
            float frequency = 250;
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(frequency);
            }

            // Act
            SingleChannelSignal actual = SingleChannelSignalReader.Read(_filePath);

            // Assert
            Assert.Equal(frequency, actual.Frequency);
            Assert.Empty(actual.Data);
        }

        [Fact]
        public void Read_EmptyFile_Throws()
        {
            // Arrange
            File.WriteAllText(_filePath, string.Empty);

            // Assert
            Assert.Throws<InvalidDataException>(
                () => SingleChannelSignalReader.Read(_filePath));
        }

        [Fact]
        public void Read_IncorrectDataSample_Throws()
        {
            // Arrange
            float frequency = 250;
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(frequency);
                writer.WriteLine(0);
                writer.WriteLine("abc");
                writer.WriteLine(0);
            }

            // Assert
            Assert.Throws<InvalidDataException>(
                () => SingleChannelSignalReader.Read(_filePath));
        }

        [Fact]
        public void Read_ZeroFrequency_Throws()
        {
            // Arrange
            float frequency = 0;
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(frequency);
                writer.WriteLine(5);
                writer.WriteLine(5);
                writer.WriteLine(5);
            }

            // Assert
            Assert.Throws<InvalidDataException>(
                () => SingleChannelSignalReader.Read(_filePath));
        }

        public void Dispose()
        {
            // выполняется после каждого теста
            File.Delete(_filePath);
        }

    }
}
