using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.TimerInterrogation;
using ULA.Interceptors.Application;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.TimerInterrogation
{
    public class AnalogTimerInterrogationService : Disposable, IAnalogTimerInterrogation
    {
        private readonly IDataLoadingService _dataLoadingService;
        private readonly IApplicationSettingsService _settingsService;
        private bool _isInterrogationInProcess;
        private Timer _updateTimer;
        private SemaphoreSlim _semaphoreSlim;
        private IRuntimeDevice _runtimeDevice;

        public AnalogTimerInterrogationService(IDataLoadingService dataLoadingService, IApplicationSettingsService settingsService)
        {
            _dataLoadingService = dataLoadingService;
            _settingsService = settingsService;
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            StopInterrogation();
            _dataLoadingService.Dispose();
            InterrogationCycleComplete = null;
            base.OnDisposing();
        }

        #endregion

        #region Implementation of ITimerInterrigationService

        public bool IsInterrogationInProcess
        {
            get { return _isInterrogationInProcess; }
        }

        public async Task<bool> StartInterrogation()
        {
            _updateTimer = new Timer(OnTimerTriggered, null, 0, _settingsService.FullTimeoutPeriod * 1000);
            _semaphoreSlim=new SemaphoreSlim(1,1);
            _isInterrogationInProcess = true;
            return true;
        }

        private async void OnTimerTriggered(object state)
        {
            if (_updateTimer == null) return;
            if (_semaphoreSlim.CurrentCount == 0) return;
            await _semaphoreSlim.WaitAsync();
           
            if (_runtimeDevice.TcpDeviceConnection == null||!_runtimeDevice.TcpDeviceConnection.LastTransactionSucceed)
            {
                return;
            }
            if (IsDisposed) return;
            if (_runtimeDevice.IsDeviceInitialized)
            {
                await _dataLoadingService?.UpdateAnalogs(_runtimeDevice);
                InterrogationCycleComplete?.Invoke();
            }
            if (_semaphoreSlim.CurrentCount == 0)
            {
                _semaphoreSlim.Release();
            }
        }

        public void StopInterrogation()
        {
            _updateTimer?.Dispose();
            _updateTimer = null;
            _isInterrogationInProcess = false;
        }

        public Action InterrogationCycleComplete { get; set; }

        public void SetDeviceForInterrogation(IRuntimeDevice runtimeDevice)
        {
            _runtimeDevice = runtimeDevice;
 
        }

        #endregion
    }
}
