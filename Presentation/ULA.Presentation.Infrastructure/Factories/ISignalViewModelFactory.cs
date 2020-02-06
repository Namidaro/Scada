using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.Infrastructure.Factories
{
    public interface ISignalViewModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customFider"></param>
        /// <returns></returns>
        ISignalDefinitionsViewModel CreateSignalDefinitionsViewModel(ICustomSignal customSignal);
    }
}