using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class SingleChannelSignalTests
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Signal_NullData_Throws()
        {
            Assert.Throws<ArgumentNullException>("data",
                () => new SingleChannelSignal(null, 200));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Signal_IncorrectFrequency_Throws(float frequency)
        {
            float[] data = new float[] { 0, 0, 0 };

            Assert.Throws<ArgumentOutOfRangeException>("frequency",
                () => new SingleChannelSignal(data, frequency));
        }
    }
}
