using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.Infrastructure.Factories
{
    /// <summary>
    /// 
    /// </summary>
   public interface IFiderViewModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customFider"></param>
        /// <returns></returns>
        IFiderDefinitionsViewModel CreateFiderDefinitionsViewModel(ICustomFider customFider);
    }
}
