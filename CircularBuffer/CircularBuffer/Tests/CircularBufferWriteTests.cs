using System;
using Xunit;

namespace PlanlyTask.CircularBuffer.Tests
{
    public class CircularBufferWriteTests
    {
        [Fact]
        public void Can_write_and_read_multiple_items()
        {
            var buffer = new CircularBuffer<int>(capacity: 3);
            buffer.Write(1);
            buffer.Write(2);
            buffer.Write(3);

            Assert.Equal(1, buffer.Read());
            Assert.Equal(2, buffer.Read());
            Assert.Equal(3, buffer.Read());
        }

        [Fact]
        public void Writing_to_a_full_buffer_should_fail()
        {
            var buffer = new CircularBuffer<int>(capacity: 2);
            buffer.Write(1);
            buffer.Write(2);

            Assert.Throws<InvalidOperationException>(() => buffer.Write(3));
        }
    }
}
