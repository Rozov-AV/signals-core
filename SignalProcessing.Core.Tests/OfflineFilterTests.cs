using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class OfflineFilterTests
    {
        [Fact]
        public void Filter_ConstantSignal_ReturnsSameConstant()
        {
            // Arrange
            float[] data = { 5, 5, 5, 5, 5 };

            // Act
            float[] result = OfflineFilter.Filter(data, 3);

            // Assert
            Assert.Equal(data.Length, result.Length);
            foreach (float value in result)
                Assert.Equal(5f, value, 1e-5f);
        }

        [Fact]
        public void Filter_AscendingSignal()
        {
            // Arrange
            float[] data = { 1, 2, 3, 4, 5 };
            float[] expected = { (float)4/3, 2, 3, 4, (float)14 / 3 };

            // Act
            float[] result = OfflineFilter.Filter(data, 3);

            // Assert
            AssertSignal.Equal(expected, result);
        }

        [Fact]
        public void Filter_ImpulseSignal()
        {
            // Arrange
            float[] data = { 0, 0, 0, 1, 0, 0, 0 };
            float[] expected = { 0, 0, (float)1 / 3, (float)1 / 3, (float)1 / 3, 0, 0 };

            // Act
            float[] result = OfflineFilter.Filter(data, 3);

            // Assert
            AssertSignal.Equal(expected, result);
        }

        [Fact]
        public void Filter_WindowSizeOne_SameValues_DifferentArray()
        {
            // Arrange
            float[] data = { 5, 5, 5, 5, 5 };

            // Act
            float[] result = OfflineFilter.Filter(data, 1);

            // Assert
            AssertSignal.Equal(data, result);
            Assert.NotSame(data, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(4)]
        public void Filter_IncorrectWindowSize_Throws(int windowSize)
        {
            // Arrange
            float[] data = { 5, 5, 5, 5, 5 };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => OfflineFilter.Filter(data, windowSize));
        }

        [Fact]
        public void Filter_NullData_Throws()
        {
            Assert.Throws<ArgumentNullException>("data",
                () => new SingleChannelSignal(null, 5));
        }

        [Fact]
        public void Filter_ZeroLengthData_ReturnZeroLength()
        {
            // Arrange
            float[] data = new float[0];

            // Act
            float[] result = OfflineFilter.Filter(data, 1);

            // Assert
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void Filter_InputArray_NotChanges()
        {
            // Arrange
            float[] data = { 1, 2, 3, 4, 5 };
            float[] dataCopy = data.ToArray();

            // Act
            float[] result = OfflineFilter.Filter(data, 3);

            // Assert
            AssertSignal.Equal(dataCopy, data);
        }
    }

}
