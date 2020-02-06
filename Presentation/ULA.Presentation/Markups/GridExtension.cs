using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ULA.Presentation.Markups
{
    /// <summary>
    ///     Класс с расширениями для Grid. используется для функционала "перетаскивания" виджетов по списку устройств
    /// </summary>
    public static class GridExtentions
    {
        /// <summary>
        ///     Метод ищет в родителях свойства указанное свойства типа "T"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T Parent<T>(this DependencyObject root) where T : class
        {
            if (root is T) { return root as T; }

            DependencyObject parent = VisualTreeHelper.GetParent(root);
            return parent != null ? parent.Parent<T>() : null;
        }

        /// <summary>
        ///     Возврашает точку как номер строк и колонок
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="relativePoint"></param>
        /// <returns></returns>
        public static Point GetColumnRow(this Grid obj, Point relativePoint) { return new Point(GetColumn(obj, relativePoint.X), GetRow(obj, relativePoint.Y)); }
        private static int GetRow(Grid obj, double relativeY) { return GetData(obj.RowDefinitions, relativeY); }
        private static int GetColumn(Grid obj, double relativeX) { return GetData(obj.ColumnDefinitions, relativeX); }

        private static int GetData<T>(IEnumerable<T> list, double value) where T : DefinitionBase
        {
            var start = 0.0;
            var result = 0;

            var property = typeof(T).GetProperties().FirstOrDefault(p => p.Name.StartsWith("Actual"));
            if (property == null) { return result; }

            foreach (var definition in list)
            {
                start += (double)property.GetValue(definition, null);
                if (value < start) { break; }

                result++;
            }

            return result;
        }
    }
}
