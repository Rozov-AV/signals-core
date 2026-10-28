using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SignalOperationsTests
    {
        [Fact]
        public void Sum_TwoSignals_DifferentLengths()
        {
            // Arrange
            float frequency = 200;
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(new float[] { 1, 2, 3 }, frequency),
                new SingleChannelSignal(new float[] { 10, 20 }, frequency)
            };
            float[] expected = new float[] { 11, 22, 3 };

            // Act
            SingleChannelSignal signalsSum = SignalOperations.Sum(signalsList);

            // Assert
            AssertSignal.Equal(expected, signalsSum.Data);
            Assert.Equal(frequency, signalsSum.Frequency, 1e-5);
        }

        [Fact]
        public void Concat_TwoSignals_DifferentLengths()
        {
            // Arrange
            float frequency = 200;
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(new float[] { 1, 2 }, frequency),
                new SingleChannelSignal(new float[] { 3 }, frequency)
            };
            float[] expected = new float[] { 1, 2, 3 };

            // Act
            SingleChannelSignal signalsConcat = SignalOperations.Concat(signalsList);

            // Assert
            AssertSignal.Equal(expected, signalsConcat.Data);
            Assert.Equal(frequency, signalsConcat.Frequency, 1e-5);
        }

        [Fact]
        public void Sum_SignalsWithDifferentFrequencies_Throws()
        {
            // Arrange
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(new float[] { 1, 2, 3 }, 100),
                new SingleChannelSignal(new float[] { 10, 20 }, 200)
            };

            // Assert
            Assert.Throws<ArgumentException>("signals",
                () => SignalOperations.Sum(signalsList));
        }

        [Fact]
        public void Concat_SignalsWithDifferentFrequencies_Throws()
        {
            // Arrange
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(new float[] { 1, 2, 3 }, 100),
                new SingleChannelSignal(new float[] { 10, 20 }, 200)
            };

            // Assert
            Assert.Throws<ArgumentException>("signals",
                () => SignalOperations.Concat(signalsList));
        }

        [Fact]
        public void Sum_EmptyList_Throws()
        {
            // Assert
            Assert.Throws<ArgumentException>("signals",
                () => SignalOperations.Sum(new List<SingleChannelSignal>()));
        }

        [Fact]
        public void Concat_EmptyList_Throws()
        {
            // Assert
            Assert.Throws<ArgumentException>("signals",
                () => SignalOperations.Concat(new List<SingleChannelSignal>()));
        }

        [Fact]
        public void Sum_NullList_Throws()
        {
            // Assert
            Assert.Throws<ArgumentNullException>("signals",
                () => SignalOperations.Sum(null));
        }

        [Fact]
        public void Concat_NullList_Throws()
        {
            // Assert
            Assert.Throws<ArgumentNullException>("signals",
                () => SignalOperations.Concat(null));
        }

        [Fact]
        public void Sum_ListWithNullSignal_Throws()
        {
            // Arrange
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(new float[] { 1, 2, 3 }, 100),
                null
            };

            // Assert
            Assert.Throws<ArgumentNullException>("signals",
                () => SignalOperations.Sum(signalsList));
        }

        [Fact]
        public void Concat_ListWithNullSignal_Throws()
        {
            // Arrange
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(new float[] { 1, 2, 3 }, 100),
                null
            };

            // Assert
            Assert.Throws<ArgumentNullException>("signals",
                () => SignalOperations.Concat(signalsList));
        }

        [Fact]
        public void Sum_TwoSignals_NotChangesSrcSignals()
        {
            // Arrange
            float frequency = 200;
            float[] data1 = new float[] { 1, 2, 3 };
            float[] data2 = new float[] { 10, 20 };
            List<SingleChannelSignal> signalsList = new List<SingleChannelSignal>()
            {
                new SingleChannelSignal(data1.ToArray(), frequency),
                new SingleChannelSignal(data2.ToArray(), frequency)
            };

            // Act
            SingleChannelSignal signalsSum = SignalOperations.Sum(signalsList);

            // Assert
            AssertSignal.Equal(data1, signalsList[0].Data);
            Assert.Equal(frequency, signalsList[0].Frequency, 1e-5);
            AssertSignal.Equal(data2, signalsList[1].Data);
            Assert.Equal(frequency, signalsList[1].Frequency, 1e-5);
        }
    }
}
