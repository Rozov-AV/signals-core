using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class CircularBuffer<T>
    {
        private readonly T[] _buffer;

        private int _start;

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Некорректная ёмкость буфера!");

            Capacity = capacity;
            Count = 0;

            _start = 0;
            _buffer = new T[capacity];
        }

        public int Capacity { get; } // ёмкость

        public int Count { get; private set; } // число хранимых элементов

        public bool IsFull { get => Count == Capacity; } // Count == Capacity
                                    // добавляет элемент; при заполненном буфере вытесняет самый старый
        public void Add(T item)
        {
            if (IsFull)
            {
                _buffer[_start % Capacity] = item;
                _start++;
                
                return;
            }

            _buffer[Count] = item;
            Count++;
        }

        // 0 – самый старый элемент, Count − 1 – самый новый
        public T this[int index]
        { 
            get
            {
                if ((index < 0) || (index >= Count))
                    throw new ArgumentOutOfRangeException(nameof(index), "Некорректный индекс!");

                return _buffer[(_start + index) % Capacity];
            }
        }

        // копия содержимого от старого к новому
        public T[] ToArray()
        {
            T[] dst = new T[Count];
            for (int i = 0; i < Count; i++)
                dst[i] = _buffer[(i + _start) % Capacity];

            return dst;
        }

        // удаляет все элементы, ёмкость сохраняется
        public void Clear()
        {
            Count = 0;
            _start = 0;
        }
    }
}
