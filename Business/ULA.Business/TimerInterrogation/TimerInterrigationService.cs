using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.TimerInterrogation;
using ULA.Interceptors.Application;
using YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.TimerInterrogation
{
    /// <summary>
    /// 
    /// </summary>
    public class TimerInterrigationService : Disposable, IDeviceTimerInterrogationService
    {
        private IDataLoadingService _dataLoadingService;
        private readonly IApplicationSettingsService _settingsService;
        private bool _isInterrogationInProcess;
        private IRuntimeDevice _runtimeDevice;
        private Timer _updateTimer;
        private bool _isFullUpdate;
        private SemaphoreSlim _semaphoreSlim;
       // private Stopwatch _stopwatch;
        public TimerInterrigationService(IDataLoadingService dataLoadingService, IApplicationSettingsService settingsService)
        {
            _dataLoadingService = dataLoadingService;
            _settingsService = settingsService;
            _semaphoreSlim=new SemaphoreSlim(1,1);
           // _stopwatch=new Stopwatch();
          //  _stopwatch.Start();
        }



        #region Implementation of ITimerInterrigationService
        /// <summary>
        /// происходит ли опрос в данный момент
        /// </summary>
        public bool IsInterrogationInProcess
        {
            get { return _isInterrogationInProcess; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartInterrogation()
        {
            if (_isFullUpdate)
            {
                _updateTimer = new Timer(OnTimerTriggered, null, 0, _settingsService.FullTimeoutPeriod*1000);
            }
            else
            {
                _updateTimer = new Timer(OnTimerTriggered, null, 0, _settingsService.PartialTimeoutPeriod * 1000);

            }
            _isInterrogationInProcess = true;
            return true;
        }


        private async void OnTimerTriggered(object state)
        {
           
            if(_updateTimer==null)return;
            if(_semaphoreSlim.CurrentCount==0)return;
            await _semaphoreSlim.WaitAsync();
            if (!_runtimeDevice.TcpDeviceConnection.LastTransactionSucceed)
            {
                if (!await _runtimeDevice.TcpDeviceConnection.OpenConnectionSession(false))
                {
                    _semaphoreSlim.Release();
                    return;
                }
                else
                {
                    if (IsDisposed) return;
                    await _runtimeDevice.InitializeAsync();
                }
            }
            if (IsDisposed) return;
            if (_runtimeDevice.IsDeviceInitialized)
            {
               
              //  Debug.Print("---------------------------------------------------------------ОБМЕН   "+_stopwatch.Elapsed.ToString()+"----------------------------------------");
              //  _stopwatch.Reset();
              //  _stopwatch.Start();
                if (_isFullUpdate)
                {
                    await _dataLoadingService?.UpdateDataFromDeviceFull(_runtimeDevice);
                }
                else
                {
                   
                    await _dataLoadingService?.UpdateDataFromDevicePartly(_runtimeDevice);
                }

                InterrogationCycleComplete?.Invoke();
            }
            if (_semaphoreSlim.CurrentCount == 0)
            {
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// остановка опроса
        /// </summary>
        public void StopInterrogation()
        {
            _updateTimer?.Dispose();
            _updateTimer = null;
            _isInterrogationInProcess = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public Action InterrogationCycleComplete { get; set; }
        public void SetDeviceForInterrogation(IRuntimeDevice runtimeDevice)
        {
            _runtimeDevice = runtimeDevice;
        }

        public void SetIsFullUpdating(bool isFull)
        {
            _isFullUpdate = isFull;
        }

        #region Overrides of Disposable

        protected override void OnDisposing()
        {
            StopInterrogation();
            _runtimeDevice = null;
            _dataLoadingService.Dispose();
            _dataLoadingService = null;
            base.OnDisposing();
            
        }

        #endregion

        #endregion
    }
}
