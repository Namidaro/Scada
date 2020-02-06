using System.Collections.Generic;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors;

namespace ULA.Business.Infrastructure.Commands
{
    /// <summary>
    /// правило, определяющее успех выполнения комманды
    /// </summary>
    public interface ICommandSuccessRule
    {
        /// <summary>
        /// выполнена ли комманда успешно
        /// </summary>
        /// <returns></returns>
        bool IsCommandSucceed(IRuntimeDeviceViewModel runtimeDeviceViewModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="starterIds"></param>
        /// <param name="commandType"></param>
        void SetRuleData(List<int> starterIds, CommandTypesEnum commandType);

    }
}