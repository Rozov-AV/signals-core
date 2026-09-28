using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParser
{
    /// <summary>
    /// Класс для сохранения сигналов в файлы.
    /// </summary>
    public static class SingleChannelSignalWriter
    {
        /// <summary>
        /// Записывает сигнал в файл.
        /// </summary>
        /// <param name="signal">Сигнал.</param>
        /// <param name="filePath">Путь к файлу для записи.</param>
        public static void Write(SingleChannelSignal signal, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(signal.Frequency);

                float[] data = signal.Data;
                for (int i = 0; i < data.Length; i++)
                    writer.WriteLine(data[i]);
            }
        }
    }
}
