using System;

namespace PlanlyTask.CircularBuffer
{
    public class CircularBuffer<T>
    {
        private readonly T[] _buffer;
        private int _head;
        private int _tail;
        private bool _isBufferFull;

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Buffer capacity must be greater than zero.", nameof(capacity));
            }

            _buffer = new T[capacity];
            _head = 0;
            _tail = 0;
            _isBufferFull = false;
        }

        public void Write(T value)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Buffer is full.");
            }

            _buffer[_head] = value;
            _head = (_head + 1) % _buffer.Length;

            if (_head == _tail)
            {
                _isBufferFull = true;
            }
        }

        public T Read()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Buffer is empty.");
            }

            var value = _buffer[_tail];
            _buffer[_tail] = default(T);  // Optional: Clear the slot after reading
            _tail = (_tail + 1) % _buffer.Length;
            _isBufferFull = false;

            return value;
        }

        public void Overwrite(T value)
        {
            if (IsFull)
            {
                _buffer[_tail] = value;
                _tail = (_tail + 1) % _buffer.Length;
                _head = (_head + 1) % _buffer.Length;
            }
            else
            {
                Write(value);
            }
        }

        public void Clear()
        {
            _head = 0;
            _tail = 0;
            _isBufferFull = false;
            Array.Clear(_buffer, 0, _buffer.Length);  // Optional: Clear the entire buffer
        }

        public bool IsEmpty => !_isBufferFull && (_head == _tail);

        public bool IsFull => _isBufferFull;
    }


}
