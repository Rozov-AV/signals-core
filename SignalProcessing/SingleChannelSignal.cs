using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    /// <summary>
    /// Класс, описывающий одноканальный сигнал.
    /// </summary>
    public class SingleChannelSignal
    {
        #region Константы

        #endregion

        #region Поля и свойства

        /// <summary>
        /// Сигнал (любые ед.)
        /// </summary>
        public float[] Data { get; }

        /// <summary>
        /// Частот (Гц).
        /// </summary>
        public float Frequency { get; }

        /// <summary>
        /// Отсчеты времени, для которых хранятся значения сигнала (с).
        /// </summary>
        public float[] TimeTicks
        {
            get
            {
                int count = Data.Length;
                float freq = Frequency;

                return Enumerable.Range(0, count).Select(i => i / freq).ToArray();
            }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="data">Сигнал (любые ед.).</param>
        /// <param name="frequency">Частота (Гц).</param>
        public SingleChannelSignal(float[] data, float frequency)
        {
            Data = data;
            Frequency = frequency;
        }

        #endregion

        #region Открытые методы

        #endregion

        #region Внутренние методы

        #endregion

    }
}
