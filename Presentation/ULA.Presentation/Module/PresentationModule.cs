using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.Presentation.Factories;
using ULA.Presentation.Infrastructure.Factories;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;
using ULA.Presentation.Infrastructure.ViewModels.Helpers;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Log;
using ULA.Presentation.Loggers;
using ULA.Presentation.ViewModels.CustomItems;
using ULA.Presentation.ViewModels.Helpers;
using ULA.Presentation.ViewModels.Interactions;
using ULA.Presentation.ViewModels.Log;

namespace ULA.Presentation.Module
{

    /// <summary>
    /// 
    /// </summary>
   public class PresentationModule:IModule
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public PresentationModule(IUnityContainer container)
        {
            _container = container;
        }


        #region Implementation of IModule
        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _container.RegisterType<IFiderDefinitionsViewModel, FiderDefinitionsViewModel>();
            _container.RegisterType<IFiderViewModelFactory, FiderViewModelFactory>();

            _container.RegisterType<ISignalDefinitionsViewModel, SignalDefinitionsViewModel>();
            _container.RegisterType<ISignalViewModelFactory, SignalViewModelFactory>();

            _container.RegisterType<IIndicatorDefinitionsViewModel, IndicatorDefinitionsViewModel>();
            _container.RegisterType<IIndicatorViewModelFactory, IndicatorViewModelFactory>();
            
            _container.RegisterType<ICascadeDefinitionsViewModel, CascadeDefinitionsViewModel>();
            _container.RegisterType<ICascadeViewModelFactory, CascadeViewModelFactory>();


            _container.RegisterType<ILogLoadingHelper, LogLoadingHelper>();
            _container.RegisterType<ILogMessageViewModelFactory, LogMessageViewModelFactory>();
            _container.RegisterType<ILogMessageViewModel,LogMessageViewModel>();
            _container.RegisterType<IApplicationLogViewModel, ApplicationLogViewModel>();
            _container.RegisterType<CityWideCommandsLogger>();


            _container.RegisterType<ICommandOnTemplateInteractionViewModel, CommandOnTemplateInteractionViewModel>();




        }

        #endregion
    }
}
