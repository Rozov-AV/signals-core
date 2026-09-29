using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalProcessing.Core
{
    public static class OfflineSingleChannelFilter
    {
        #region Поля и свойства



        #endregion

        #region Конструкторы

        #endregion

        #region Открытые методы

        public static float[] Filter(float[] data, int windowSize)
        {
            int length = data.Length;
            int windowRadius = windowSize / 2;
            int extLength = length + 2 * windowRadius;
            float[] extData = Enumerable.Range(0, windowRadius).Select(_ => data.First())
                .Concat(data)
                .Concat(Enumerable.Range(0, windowRadius).Select(_ => data.Last()))
                .ToArray();

            float[] res = new float[length];
            int windowLength = 2 * windowRadius + 1;
            float sum = extData.Take(windowLength).Sum();
            for (int i = 0; i < length; i++)
            {
                res[i] = sum / windowLength;
                if (i < length - 1)
                    sum += extData[i + windowLength] - extData[i];
            }

            return res;
        }

        #endregion
    }
}
