using System;
using System.Windows;
using System.Windows.Threading;

namespace ULA.Common
{/// <summary>
/// сервис для вызова в потоке контрола
/// </summary>
    public static class DispatchService
    {/// <summary>
    /// вызова в потоке контрола
    /// </summary>
    /// <param name="action"></param>
        public static void Invoke(Action action)
        {
            Dispatcher dispatchObject =Application.Current.Dispatcher;
            if (dispatchObject == null || dispatchObject.CheckAccess())
            {
                action();
            }
            else
            {
                dispatchObject.Invoke(action);
            }
        }


    }
}
