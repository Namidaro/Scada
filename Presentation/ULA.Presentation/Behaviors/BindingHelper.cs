using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ULA.Presentation.Behaviors
{
    /// <summary>
    ///     Represents a binding helper that provides some basic binding help methods
    ///     Представляет Помощник привязки, который обеспечивает некоторые базовые методы помогающие при привязке
    /// </summary>
    public static class BindingHelper
    {
        #region [Public members]

        /// <summary>
        ///     Updates a binding for an object
        ///     Обновляет привязку к объекту
        /// </summary>
        /// <param name="item">An object to update binding for</param>
        /// <param name="property">The property to update</param>
        /// <param name="dataContext">A binding source to use</param>
        public static void UpdateBinding(DependencyObject item, DependencyProperty property, object dataContext)
        {
            BindingExpression bindingExpression = BindingOperations.GetBindingExpression(item, property);
            if (bindingExpression == null) return;
            Binding parentBinding = bindingExpression.ParentBinding;

            string propertyName = parentBinding.Path.Path;
            if (dataContext == null) return;
            PropertyInfo dataContextProperty = dataContext.GetType().GetProperty(propertyName);
            if (dataContextProperty == null) return;
            var commandValue = dataContextProperty.GetValue(dataContext, null) as ICommand;
            if (commandValue == null) return;
            item.SetValue(property, commandValue);
        }

        #endregion [Public members]
    }
}