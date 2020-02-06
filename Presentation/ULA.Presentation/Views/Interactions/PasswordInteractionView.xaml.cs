using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace ULA.Presentation.Views.Interactions
{
    /// <summary>
    /// Interaction logic for PasswordInteractionView.xaml
    /// </summary>
    public partial class PasswordInteractionView
    {
        /// <summary>
        ///     Creates a instance of <see cref="PasswordInteractionView"/>
        /// </summary>
        public PasswordInteractionView()
        {
            InitializeComponent();
            this.IsVisibleChanged +=
                LoginControl_IsVisibleChanged;
        }

        void LoginControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(() => MyPasswordBox.Focus()));
            }
        }

        private void MyPasswordBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OkButton.Command.Execute(null);
            }
        }
    }
}
