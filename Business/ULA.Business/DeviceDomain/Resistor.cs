using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.DeviceDomain
{
    /// <summary>
    /// 
    /// </summary>
   public class Resistor:IResistor
    {
        public Resistor()
        {
            
        }


        #region Implementation of IResistor

        public int ModuleNumber { get; set; }
        public int ResistorNumber { get; set; }
        public bool IsDefectState { get; set; }
        public bool IsOnState { get; set; }

        public IStarter ParentStarter { get; set; }

        #endregion
    }
}
