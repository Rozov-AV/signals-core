using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class OnlineFilterTests
    {
        [Fact]
        public void Filter_SingleCall()
        {
            // Arrange
            float[] data = { 1, 2, 3, 4, 5 };
            float[] expected = { 1, (float)4 / 3, 2, 3, 4 };
            OnlineFilter filter = new OnlineFilter(3);

            // Act
            float[] result = filter.Filter(data);

            // Assert
            AssertSignal.Equal(expected, result);
        }

        [Fact]
        public void Filter_DoubleCall()
        {
            // Arrange
            float[] data1 = { 1, 2 };
            float[] data2 = { 3, 4, 5 };
            float[] expected = { 1, (float)4 / 3, 2, 3, 4 };
            OnlineFilter filter = new OnlineFilter(3);

            // Act
            float[] result1 = filter.Filter(data1);
            float[] result2 = filter.Filter(data2);
            float[] result = result1.Concat(result2).ToArray();

            // Assert
            AssertSignal.Equal(expected, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(100)]
        public void Filter_DifferentPortionSizes_SameResult(int portionSize)
        {
            // Arrange
            Random rand = new Random(42);
            int length = 1000;
            float[] data = new float[length];
            for (int i = 0; i < length; i++)
                data[i] = (float)rand.NextDouble();

            int windowSize = 7;
            OnlineFilter filter1 = new OnlineFilter(windowSize);
            float[] expected = filter1.Filter(data);

            // Act
            OnlineFilter filter2 = new OnlineFilter(windowSize);
            int ind = 0;
            List<float> actualList = new List<float>();
            while (ind < length)
            {
                int n = Math.Min(portionSize, length - ind);
                actualList.AddRange(filter2.Filter(data.Skip(ind).Take(n).ToArray()));
                ind += n;
            }

            // Assert
            AssertSignal.Equal(expected, actualList.ToArray());
        }

        [Fact]
        public void Filter_OnlineFilter_EqualsShiftedOfflineFilter()
        {
            // Arrange
            Random rand = new Random(42);
            int length = 1000;
            float[] data = new float[length];
            for (int i = 0; i < length; i++)
                data[i] = (float)rand.NextDouble();

            // Act
            int windowSize = 7;
            OnlineFilter filter1 = new OnlineFilter(windowSize);
            float[] resultOnline = filter1.Filter(data);

            float[] resultOffline = OfflineFilter.Filter(data, windowSize);

            // Assert
            for (int i = 3; i < length; i++)
                Assert.Equal(resultOffline[i - 3], resultOnline[i], 1e-5f);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Filter_IncorrectWindowSize_Throws(int windowSize)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>("windowSize",
                () => new OnlineFilter(windowSize));
        }
    }
}
