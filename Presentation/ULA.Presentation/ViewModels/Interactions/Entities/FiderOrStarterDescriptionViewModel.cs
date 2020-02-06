using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Prism.Mvvm;

namespace ULA.Presentation.ViewModels.Interactions.Entities
{
    /// <summary>      
    ///   Вью модель описания фидера или стартера для отображения
    /// </summary>
    public class StarterDescriptionViewModel:BindableBase
    {
        private bool _isStartedEnabled;
        private uint _channelNumberOfStarter;
        private uint _defaultChannelNumberOfStarter;

        /// <summary>      
        ///  Вью модель описания фидера для отображения
        /// </summary>
        public StarterDescriptionViewModel()
        {
            Description=string.Empty;
            IndexAndType=string.Empty;
            IndexAndType = string.Empty;
        }
        /// <summary>      
        ///  Само описание фидера или стартера
        /// </summary>
        public string Description { get; set; }

        /// <summary>      
        ///  То, что выведется на лэйблу
        /// </summary>
        public string IndexAndType { get; set; }

        /// <summary>      
        ///  канал
        /// </summary>
        public uint ChannelNumberOfStarter
        {
            get { return _channelNumberOfStarter; }
            set
            {
                _channelNumberOfStarter = value;
                RaisePropertyChanged();
            }
        }




        public void SetDefaultChannelNumberOfStarter(uint num)
        {
            _defaultChannelNumberOfStarter = num;
        }
        public void SetChannelNumberOfStarter(uint num)
       {
           ChannelNumberOfStarter = num;
           IsStartedEnabled = num != 0;
       }

        public bool IsStartedEnabled
        {
            get { return _isStartedEnabled; }
            set
            {
                _isStartedEnabled = value;
                if (value == false)
                {
                    ChannelNumberOfStarter = 0;
                }
                RaisePropertyChanged();
            }
        }
    }
}
