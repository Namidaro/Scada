using System;
using System.Windows.Markup;
using ULA.Localization;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.SharedResourses.Markups
{
    /// <summary>
    ///     Represents localization resources accessor markup for xaml usage
    ///     представляет класс через который можно получить доступ к ресурсом через xaml
    /// </summary>
    [MarkupExtensionReturnType(typeof (string))]
    public class LocalizationResourcesExtension : MarkupExtension
    {
        #region [Properties]

        /// <summary>
        ///     Gets or sets a resource identifier
        /// </summary>
        [ConstructorArgument("resource")]
        public string Resource { get; set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LocalizationResourcesExtension" />
        /// </summary>
        /// <param name="resource">A identifier of the resource to obtain from the localization resources registry</param>
        public LocalizationResourcesExtension(string resource)
        {
            Guard.AgainstEmptyStringOrNull(resource, "resource");
            this.Resource = resource;
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     When implemented in a derived class, returns an object that is provided as the value of the target property for
        ///     this markup extension.
        /// </summary>
        /// <returns>
        ///     The object value to set on the property where the extension is applied.
        /// </returns>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return LocalizationResources.Instance[this.Resource];
        }

        #endregion [Override members]
    }
}