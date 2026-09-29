using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public class OnlineSingleChannelFilter
    {
        #region Поля и свойства

        /// <summary>
        /// Флаг, определяющий активность фильтра.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Размер окна для усреднения (отсчеты).
        /// </summary>
        private readonly int _windowSize;

        /// <summary>
        /// Буфер с сигналом для усреднения.
        /// </summary>
        private  float[] _dataBuffer;

        /// <summary>
        /// Число уже отфильтрованных отсчетов.
        /// </summary>
        private int _index;

        #endregion

        #region Конструкторы

        public OnlineSingleChannelFilter(int windowSize)
        {
            _windowSize = windowSize;
            _index = 0;
        }

        #endregion

        #region Открытые методы

        /// <summary>
        /// Фильтрует сигнал.
        /// </summary>
        /// <param name="data">Фильтруемый фрагмент сигнала.</param>
        /// <returns>Фильтрованный фрагмент сигнала.</returns>
        public float[] Filter(float[] data)
        {
            if (!Active)
            {
                return data;
            }

            if (_index == 0)
            {
                _dataBuffer = Enumerable.Range(0, _windowSize).Select(_ => data.First()).ToArray();
            }

            int count = data.Length;
            float[] filteredData = new float[count];
            for (int i = 0; i < count; i++)
            {
                _dataBuffer[_index % _windowSize] = data[i];
                _index++;
                filteredData[i] = _dataBuffer.Average();
            }

            return filteredData;
        }

        /// <summary>
        /// Выполняет сброс фильтра.
        /// </summary>
        public void Reset()
        {
            _index = 0;
            _dataBuffer = null;
        }

        #endregion

        #region Внутренние методы

        #endregion

    }
}
