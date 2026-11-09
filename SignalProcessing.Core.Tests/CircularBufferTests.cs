using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [Fact]
        public void Add_TwoElements_IsNotFull()
        {
            // Arrange
            int capacity = 4;
            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);
            int[] src = new int[] { 1, 2 };

            // Act
            buffer.Add(src[0]);
            buffer.Add(src[1]);

            // Assert
            Assert.Equal(2, buffer.Count);
            Assert.False(buffer.IsFull);
            Assert.Equal(src, buffer.ToArray());
        }

        [Fact]
        public void Add_CapacityElements_IsFull()
        {
            // Arrange
            int capacity = 4;
            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);
            int[] src = new int[] { 1, 2, 3, 4 };

            // Act
            foreach (int k in src)
                buffer.Add(k);

            // Assert
            Assert.True(buffer.IsFull);
        }

        [Fact]
        public void CapacityOne_AddBeyondCapacityElements_LastStored()
        {
            // Arrange
            int capacity = 1;
            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);

            // Act
            for (int i = 0; i < 3; i++)
                buffer.Add(i);

            // Assert
            Assert.Equal(new int[] { 2 }, buffer.ToArray());
        }

        [Fact]
        public void AddThousandElements_LastStored()
        {
            // Arrange
            int capacity = 7;
            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);

            // Act
            for (int i = 1; i <= 1000; i++)
                buffer.Add(i);

            // Assert
            Assert.Equal(new int[] { 994, 995, 996, 997, 998, 999, 1000 }, buffer.ToArray());
        }

        [Fact]
        public void Clear_ThenAddOneElement()
        {
            // Arrange
            int capacity = 7;
            int x = -7;
            CircularBuffer<int> buffer = new CircularBuffer<int>(capacity);

            // Act
            for (int i = 1; i <= 1000; i++)
                buffer.Add(i);
            buffer.Clear();
            buffer.Add(x);

            // Assert
            Assert.Equal(1, buffer.Count);
            Assert.Equal(x, buffer[0]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_InvalidCapacity_Throws(int capacity)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CircularBuffer<int>(capacity));
        }
    }
}
