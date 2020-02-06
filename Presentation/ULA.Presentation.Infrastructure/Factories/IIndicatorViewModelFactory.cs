using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.Infrastructure.Factories
{
    public interface IIndicatorViewModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customFider"></param>
        /// <returns></returns>
        IIndicatorDefinitionsViewModel CreateIndicatorDefinitionsViewModel(ICustomIndicator customIndicator);
    }
}