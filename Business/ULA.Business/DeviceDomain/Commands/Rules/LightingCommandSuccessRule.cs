using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors;

namespace ULA.Business.DeviceDomain.Commands.Rules
{
   public class LightingCommandSuccessRule: ICommandSuccessRule
    {
        private List<int> _starterIds;
        private CommandTypesEnum _commandType;

        #region Implementation of ICommandSuccessRule

        public bool IsCommandSucceed(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            foreach (var starterId in _starterIds)
            {
                var starter = runtimeDeviceViewModel.StarterViewModels.First((s => s.StarterNumber == starterId));
                switch (_commandType)
                {
                    case CommandTypesEnum.ON:
                        if (!starter.IsStarterOn.HasValue) return false;
                        if (!starter.IsStarterOn.Value) return false;
                        break;
                    case CommandTypesEnum.OFF:
                        if (!starter.IsStarterOn.HasValue) return false;
                        if (starter.IsStarterOn.Value) return false;
                        break;
                    case CommandTypesEnum.AUTO:
                        if (!starter.IsInManualMode.HasValue) return false;
                        if (starter.IsInManualMode.Value) return false;
                        break;
                    case CommandTypesEnum.MANUAL:
                        if (!starter.IsInManualMode.HasValue) return false;
                        if (!starter.IsInManualMode.Value) return false;
                        break;
                    case CommandTypesEnum.Repair:
                        if (!starter.IsInRepairState.HasValue) return false;
                        if (!starter.IsInRepairState.Value) return false;
                        break;
                    case CommandTypesEnum.NoRepair:
                        if (!starter.IsInRepairState.HasValue) return false;
                        if (starter.IsInRepairState.Value) return false;
                        break;
                }
            }
            return true;
        }

        public void SetRuleData(List<int> starterIds, CommandTypesEnum commandType)
        {
            _starterIds = starterIds;
            _commandType = commandType;
        }

        #endregion
    }
}
