using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
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
                if (!float.TryParse(reader.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out freq))
                    throw new InvalidDataException("Ошибка формата файла: строка 0!");
                if (freq < 1)
                    throw new InvalidDataException("Некорректная частота квантования: строка 0!");

                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;

                    if (!float.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out float s))
                        throw new InvalidDataException($"Ошибка формата файла: строка {signalList.Count + 1}!");

                    signalList.Add(s);
                }
            }

            return new SingleChannelSignal(signalList.ToArray(), freq);
        }
    }
}
