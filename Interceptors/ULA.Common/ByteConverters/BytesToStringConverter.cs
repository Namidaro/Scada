using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULA.Common.ByteConverters
{
    /// <summary>
/// класс конверитования информации из байтов
/// </summary>
   public static class BytesToStringConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringFromBytes(byte[] value)
        {
            string tmp = string.Empty;

            for (int i = 0; i < value.Length; i++)
            {
                int index = i;
                if ((i + 1)%2 == 0)
                {
                    index--;
                }
                else
                {
                    index++;
                }
                if ((value[index] != byte.MaxValue) && (value[index] != byte.MinValue)&&(value[index]!=37))
                    tmp += Convert.ToChar(value[index]);
            }
            return tmp;
        }

    }
}
