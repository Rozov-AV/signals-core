using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class SignalOperations
    {
        // поэлементная сумма сигналов
        public static SingleChannelSignal Sum(
            IReadOnlyList<SingleChannelSignal> signals)
        {
            #region Проверка входных данных

            if (signals == null)
                throw new ArgumentNullException(nameof(signals), "Список кривых null!");
            
            if (signals.Any(s => s is null))
                throw new ArgumentNullException(nameof(signals), "Список содержит кривую null!");

            int count = signals.Count;
            if (count < 1)
                throw new ArgumentException("Список кривых пуст!", nameof(signals));

            float frequency = signals.First().Frequency;
            for (int i = 1; i < count; i++)
            {
                if (Math.Abs(signals[i].Frequency - frequency) > 1e-5)
                    throw new ArgumentException($"Кривые имеют разную частоту: {signals[i].Frequency} и {frequency}!",
                        nameof(signals));
            }

            #endregion

            int length = signals.Select(s => s.Data.Length).Max();
            float[] resData = new float[length];
            for (int signalInd = 0; signalInd < count; signalInd++)
            {
                float[] data = signals[signalInd].Data;
                for (int sampleInd = 0; sampleInd < signals[signalInd].Data.Length; sampleInd++)
                {
                    resData[sampleInd] += data[sampleInd];
                }
            }

            return new SingleChannelSignal(resData, frequency);
        }

        // последовательная склейка сигналов в порядке следования в списке
        public static SingleChannelSignal Concat(
            IReadOnlyList<SingleChannelSignal> signals)
        {
            #region Проверка входных данных

            if (signals == null)
                throw new ArgumentNullException(nameof(signals), "Список кривых null!");

            if (signals.Any(s => s is null))
                throw new ArgumentNullException(nameof(signals), "Список содержит кривую null!");

            int count = signals.Count;
            if (count < 1)
                throw new ArgumentException("Список кривых пуст!", nameof(signals));

            float frequency = signals.First().Frequency;
            for (int i = 1; i < count; i++)
            {
                if (Math.Abs(signals[i].Frequency - frequency) > 1e-5)
                    throw new ArgumentException($"Кривые имеют разную частоту: {signals[i].Frequency} и {frequency}!",
                        nameof(signals));
            }

            #endregion

            IEnumerable<float> resData = new float[0];
            for (int signalInd = 0; signalInd < count; signalInd++)
            {
                resData = resData.Concat(signals[signalInd].Data);
            }

            return new SingleChannelSignal(resData.ToArray(), frequency);
        }
    }

}
