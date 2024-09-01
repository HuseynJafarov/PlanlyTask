using System;
using Xunit;

namespace PlanlyTask.CircularBuffer.Tests
{
    public class CircularBufferOverwriteTests
    {
        [Fact]
        public void Can_overwrite_oldest_item_in_full_buffer()
        {
            var buffer = new CircularBuffer<int>(capacity: 2);
            buffer.Write(1);
            buffer.Write(2);
            buffer.Overwrite(3);

            Assert.Equal(2, buffer.Read());
            Assert.Equal(3, buffer.Read());
        }
    }
}
