using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ULA.Presentation.Behaviors
{
    /// <summary>
    ///     Поведение для считывания пароля с PasswordBox в MVVM-архитектуре
    /// </summary>
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        /// <summary>
        ///     DependencyProperty для passwod поля в PasswordBox контроле
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBehavior), new PropertyMetadata(default(string)));

        private bool _skipUpdate;

        /// <summary>
        ///     Свойство доступа к PasswordProperty
        /// </summary>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        ///     Действие выполняемое при привязке поведения
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += PasswordBox_PasswordChanged;
        }

        /// <summary>
        ///     Действие выполняемое при отмене привязке поведения
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= PasswordBox_PasswordChanged;
        }

        /// <summary>
        ///     Изменение свойства
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == PasswordProperty)
            {
                if (!_skipUpdate)
                {
                    _skipUpdate = true;
                    AssociatedObject.Password = e.NewValue as string;
                    _skipUpdate = false;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _skipUpdate = true;
            Password = AssociatedObject.Password;
            _skipUpdate = false;
        }
    }
}
