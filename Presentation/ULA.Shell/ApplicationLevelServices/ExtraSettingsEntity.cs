using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULA.Shell.ApplicationLevelServices
{
    /// <summary>
    /// сущность найстроек для сохранения
    /// </summary>
    [Serializable]
   
   public class ExtraSettingsEntity
    {
        /// <summary>
        /// автоквитировнаие
        /// </summary>
        public bool AcknowledgeEnabled { get; set; }
        /// <summary>
        /// период обновления на схеме
        /// </summary>
        public int FullTimeoutPeriod { get; set; }
        /// <summary>
        /// период обновления в списке
        /// </summary>
        public int PartialTimeoutPeriod { get; set; }
        /// <summary>
        /// таймаут обменов
        /// </summary>
        public int QueryTimeoutPeriod { get; set; }



        /// <summary>
        /// повторения комманд освещения
        /// </summary>
      public  int NumberOfLightingCommandRepeat { get; set; }

        /// <summary>
        /// интервал повторения комманд освещения
        /// </summary>
      public  int MillisecondRepeatInterval { get; set; }


    }
}
