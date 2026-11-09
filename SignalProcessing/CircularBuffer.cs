using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public sealed class CircularBuffer<T>
    {
        public CircularBuffer(int capacity)
        {
            throw new NotImplementedException();
        }

        public int Capacity { get; } // ёмкость

        public int Count { get; } // число хранимых элементов

        public bool IsFull { get; } // Count == Capacity
                                    // добавляет элемент; при заполненном буфере вытесняет самый старый
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        // 0 – самый старый элемент, Count − 1 – самый новый
        public T this[int index]
        { 
            get
            {
                throw new NotImplementedException();
            }
        }

        // копия содержимого от старого к новому
        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        // удаляет все элементы, ёмкость сохраняется
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
