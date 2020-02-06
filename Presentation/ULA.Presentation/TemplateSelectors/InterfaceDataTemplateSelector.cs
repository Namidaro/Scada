using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ULA.Presentation.TemplateSelectors
{
    /// <summary>
    ///     Represents interface base data template selector
    ///     Представляет класс выбора шаблонов данных
    /// </summary>
    public class InterfaceDataTemplateSelector : DataTemplateSelector
    {
        #region [Properties]

        /// <summary>
        ///     Gets or sets a collection of <see cref="DataTemplate" /> to choose from while template selectiopn action
        ///     Коллекция Шаблонов данных
        /// </summary>
        public List<DataTemplate> DataTemplates { get; set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="InterfaceDataTemplateSelector" />
        /// </summary>
        public InterfaceDataTemplateSelector()
        {
            this.DataTemplates = new List<DataTemplate>();
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.
        ///     Выбирает шаблон
        /// </summary>
        /// <returns>
        ///     Returns a <see cref="T:System.Windows.DataTemplate" /> or null. The default value is null.
        /// </returns>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return null;
            DataTemplate template = this.DataTemplates.FirstOrDefault(f =>
            {
                var type = f.DataType as Type;
                return type != null && type.IsInstanceOfType(item);
            });
            return template;
        }

        #endregion [Override members]
    }
}