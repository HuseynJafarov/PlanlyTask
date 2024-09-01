using System;
using Xunit;

namespace PlanlyTask.CircularBuffer.Tests
{
    public class CircularBufferClearTests
    {
        [Fact]
        public void Can_clear_buffer()
        {
            var buffer = new CircularBuffer<int>(capacity: 3);
            buffer.Write(1);
            buffer.Write(2);
            buffer.Clear();

            Assert.Throws<InvalidOperationException>(() => buffer.Read());
        }
    }
}
