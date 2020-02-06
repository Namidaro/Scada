using System.Collections.Generic;
using ULA.Interceptors;

namespace ULA.Business.Infrastructure.Commands
{
    /// <summary>
    /// данные для комманды
    /// </summary>
   public class DeviceCommandData
    {
        /// <summary>
        /// индексы пускателей
        /// </summary>
        public List<int> StarterIndexes { get; set; }
        /// <summary>
        /// состояние пускателей
        /// </summary>
        public CommandTypesEnum State { get; set; }
        /// <summary>
        /// тэги данных
        /// </summary>
        public List<string> Tags { get; set; }
        /// <summary>
        /// значения данных
        /// </summary>
        public List<object> Values { get; set; }
    }
}
