using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Commands;
using ULA.Devices.Presentation.Runtime;

namespace ULA.Devices.Presentation.DataTemplateSelectors
{
   public class TimeModeDateTemplateSelector:DataTemplateSelector
    {
        
        public DataTemplate NoTimeModeDataTemplate { get; set; }
        public DataTemplate TimeModeDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var device = (item as RuntimeDeviceViewModel);

            if (device == null) return null;
            device.IsTimeModeChanged=new DelegateCommand((() =>
            {
                var cp = (ContentPresenter)container;
                cp.ContentTemplateSelector = null;
                cp.ContentTemplateSelector = this;
            }));
         
            return device.IsTimeMode ? TimeModeDataTemplate : NoTimeModeDataTemplate;
        }
    }
}
