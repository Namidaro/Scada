using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.Infrastructure.Factories
{
    public interface ICascadeViewModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cascadeIndicator"></param>
        /// <returns></returns>
        ICascadeDefinitionsViewModel CreateCascadeDefinitionsViewModel(ICascadeIndicator cascadeIndicator);
    }
}