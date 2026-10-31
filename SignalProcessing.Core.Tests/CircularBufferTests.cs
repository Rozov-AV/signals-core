using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SignalProcessing.Core.Tests
{
    public class CircularBufferTests
    {
        [Fact]
        public void Add_BeyondCapacity_OldestElementsAreDropped()
        {
            var buffer = new CircularBuffer<int>(3);
            for (int i = 1; i <= 5; i++)
                buffer.Add(i);
            Assert.Equal(3, buffer.Count);
            Assert.Equal(new[] { 3, 4, 5 }, buffer.ToArray());
            Assert.Equal(3, buffer[0]);
            Assert.Equal(5, buffer[2]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        public void Indexer_OutOfRange_Throws(int index)
        {
            var buffer = new CircularBuffer<int>(5);
            buffer.Add(10);
            buffer.Add(20);
            Assert.Throws<ArgumentOutOfRangeException>(() => buffer[index]);
        }

        [Fact]
        public void NewBuffer_IsEmpty()
        {
            int capacity = 4;
            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);

            Assert.Equal(capacity, buffer.Capacity);
            Assert.Equal(0, buffer.Count);
            Assert.False(buffer.IsFull);
            Assert.Empty(buffer.ToArray());
        }
    }
}
