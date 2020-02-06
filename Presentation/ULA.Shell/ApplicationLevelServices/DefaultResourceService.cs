using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using ULA.Presentation.Infrastructure.Services;

namespace ULA.Shell.ApplicationLevelServices
{
    /// <summary>
    ///     Represents system default resource service functionality
    ///     Сервис ресурсов
    /// </summary>
    public class DefaultResourceService : IResourcesService
    {
        #region Implementation of IResourcesService

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sourceAssembly"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddResourceAsGlobal(string filePath, Assembly sourceAssembly)
        {
            var uriPath = string.Format(CultureInfo.InvariantCulture,"pack://application:,,,/{0};component/{1}", sourceAssembly.GetName().Name,
                filePath);
            
            var packUri = new Uri(uriPath, UriKind.Absolute);
            if (packUri == null) throw new ArgumentNullException("packUri");

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = packUri
            });
        }

        #endregion
    }
}