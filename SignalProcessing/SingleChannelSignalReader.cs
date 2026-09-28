using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParser
{
    /// <summary>
    /// Класс для чтения сигналов из файлов.
    /// </summary>
    public static class SingleChannelSignalReader
    {
        /// <summary>
        /// Читает сигнал из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу для чтения.</param>
        /// <returns>Сигнал.</returns>
        public static SingleChannelSignal Read(string filePath)
        {
            List<float> signalList = new List<float>();
            float freq = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                freq = float.Parse(reader.ReadLine(), CultureInfo.InvariantCulture);
                
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    signalList.Add(float.Parse(line));
                }
            }

            return new SingleChannelSignal(signalList.ToArray(), freq);
        }
    }
}
