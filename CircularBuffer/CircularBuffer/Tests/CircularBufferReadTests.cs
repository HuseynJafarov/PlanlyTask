using System;
using Xunit;

namespace PlanlyTask.CircularBuffer.Tests
{
   
    public class CircularBufferReadTests
    {
        [Fact]
        public void Reading_empty_buffer_should_fail()
        {
            var buffer = new CircularBuffer<int>(capacity: 1);
            Assert.Throws<InvalidOperationException>(() => buffer.Read());
        }

        [Fact]
        public void Can_read_an_item_just_written()
        {
            var buffer = new CircularBuffer<int>(capacity: 1);
            buffer.Write(1);
            Assert.Equal(1, buffer.Read());
        }
    }

}
