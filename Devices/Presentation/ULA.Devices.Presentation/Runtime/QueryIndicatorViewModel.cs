using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Prism.Mvvm;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Devices.Presentation.Interfaces;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime
{
    public class QueryIndicatorViewModel : BindableBase, IQueryIndicatorViewModel
    {
        private double _indicatorOpacity;
        private SemaphoreSlim _semaphoreSlim;

        public QueryIndicatorViewModel()
        {
            _semaphoreSlim = new SemaphoreSlim(1);
            IndicatorOpacity = 0.2;
        }


        public async Task BeginIndication()
        {
            try
            {
                if (_semaphoreSlim.CurrentCount > 0)
                {
                    await _semaphoreSlim.WaitAsync();

                    IndicatorOpacity = 1;
                    await Task.Delay(300);
                    IndicatorOpacity = 0.2;
                    _semaphoreSlim.Release(1);
                }
            }
            finally
            {
                _semaphoreSlim.Release(1);
            }

        }


        public double IndicatorOpacity
        {
            get { return _indicatorOpacity; }
            set
            {
                _indicatorOpacity = value;
                RaisePropertyChanged();
            }
        }

    }
}
