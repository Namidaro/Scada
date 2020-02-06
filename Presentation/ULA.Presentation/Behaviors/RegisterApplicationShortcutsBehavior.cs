using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ULA.Presentation.Behaviors
{
    /// <summary>
    ///     Represents the behavior for registering application shortcuts globaly
    ///     Представляет поведение для глобальной регистрации горячих клавиш в приложении
    /// </summary>
    public class RegisterAppShortcutKeysBehavior : Behavior<FrameworkElement>
    {
        #region [Static Fields]

        private static readonly Dictionary<Window, List<FrameworkElement>> _associatedWindows =
            new Dictionary<Window, List<FrameworkElement>>();

        private static readonly object _associatedWindowsLock = new object();
        private static readonly List<InputBinding> _appInputBindings = new List<InputBinding>();
        private static InputBinding[] _readOnlyAppInputBindings = new InputBinding[0];
        private static readonly object _appInputBindingsLock = new object();
        private static readonly HashSet<Key> _shortcutKeys = new HashSet<Key>();

        #endregion [Static Fields]

        #region [Fields]

        private FrameworkElement _frameworkElement;

        #endregion [Fields]

        #region [Dependency properties]

        /// <summary>
        ///     Gets a InputBindingsProperty metadata
        ///     
        /// </summary>
        public static readonly DependencyProperty InputBindingsProperty =
            DependencyProperty.Register("InputBindings", typeof (InputBindingCollection),
                typeof (RegisterAppShortcutKeysBehavior),
                new FrameworkPropertyMetadata(null));


        /// <summary>
        ///     Gets or sets the input bindings.
        /// </summary>
        /// <value>The input bindings.</value>
        public InputBindingCollection InputBindings
        {
            get { return (InputBindingCollection) GetValue(InputBindingsProperty); }
            set { SetValue(InputBindingsProperty, value); }
        }

        #endregion [Dependency properties]

        #region [Ctor's]

        /// <summary>
        ///     Initializes a type <see cref="RegisterAppShortcutKeysBehavior" />
        /// </summary>
        static RegisterAppShortcutKeysBehavior()
        {
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F1);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F2);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F3);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F4);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F5);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F6);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F7);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F8);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F9);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F10);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F11);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F12);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F13);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F14);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F15);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F16);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F17);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F18);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F19);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F20);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F21);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F22);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F23);
            RegisterAppShortcutKeysBehavior._shortcutKeys.Add(Key.F24);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterAppShortcutKeysBehavior" /> class.
        /// </summary>
        public RegisterAppShortcutKeysBehavior()
        {
            InputBindings = new InputBindingCollection();
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        ///     Вызывается после того как поведение привяжется к AssociatedObject
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            this._frameworkElement = AssociatedObject;
            this._frameworkElement.DataContextChanged += OnDataContextChanged;
            this._frameworkElement.Loaded += OnElementLoaded;
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        ///     Вызывается когда поведение отвязывается от AssociatedObject, но до полной потери связи м/у поведение и AssociatedObject
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            if (this._frameworkElement != null)
            {
                UnregisterAssociatedWindow(this._frameworkElement);
                this._frameworkElement.DataContextChanged -= OnDataContextChanged;
                this._frameworkElement.Loaded -= OnElementLoaded;
            }
            base.OnDetaching();
        }

        #endregion [Override members]

        #region [Help members]

        /// <summary>
        ///     Регистрируе привязку
        /// </summary>
        /// <param name="inputBindings"></param>
        /// <param name="commandTarget"></param>
        private static void RegisterBindings(InputBindingCollection inputBindings, FrameworkElement commandTarget)
        {
            Debug.Assert(inputBindings != null);
            Debug.Assert(commandTarget != null);

            lock (RegisterAppShortcutKeysBehavior._appInputBindingsLock)
            {
                foreach (InputBinding inputBinding in inputBindings)
                {
                    if (inputBinding.Command == null)
                    {
                        BindingHelper.UpdateBinding(inputBinding, InputBinding.CommandProperty,
                            commandTarget.DataContext);
                    }
                    // I only add input bindings that are KeyGestures and have a command.
                    if ((!(inputBinding.Gesture is KeyGesture)) || (inputBinding.Command == null)) continue;
                    inputBinding.CommandTarget = commandTarget;
                    RegisterAppShortcutKeysBehavior._appInputBindings.Add(inputBinding);
                }

                RegisterAppShortcutKeysBehavior._readOnlyAppInputBindings = RegisterAppShortcutKeysBehavior._appInputBindings.ToArray();
            }
        }

        /// <summary>
        ///     Возвращает окно, которому принадлежит элемент
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        private static Window GetParentWindow(DependencyObject child)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            var parent = parentObject as Window;
            return parent ?? GetParentWindow(parentObject);
        }

        /// <summary>
        ///     Привязывает команду к окну
        /// </summary>
        /// <param name="commandTarget"></param>
        private static void RegisterAssociatedWindow(FrameworkElement commandTarget)
        {
            Window window = GetParentWindow(commandTarget);
            if (window == null) return;
            lock (RegisterAppShortcutKeysBehavior._associatedWindowsLock)
            {
                // I reference count the windows to prevent double subscribing to PreviewKeyDown
                List<FrameworkElement> frameworkElements;
                if (!RegisterAppShortcutKeysBehavior._associatedWindows.TryGetValue(window, out frameworkElements))
                {
                    frameworkElements = new List<FrameworkElement>();
                    RegisterAppShortcutKeysBehavior._associatedWindows[window] = frameworkElements;
                    window.PreviewKeyDown += Window_PreviewKeyDown;
                }

                if (!frameworkElements.Contains(commandTarget))
                {
                    frameworkElements.Add(commandTarget);
                }
            }
        }

        /// <summary>
        ///     Отвязывает команду от окна
        /// </summary>
        /// <param name="commandTarget"></param>
        private static void UnregisterAssociatedWindow(FrameworkElement commandTarget)
        {
            Debug.Assert(commandTarget != null);

            Window window = Window.GetWindow(commandTarget);
            if (window == null) return;
            lock (RegisterAppShortcutKeysBehavior._associatedWindowsLock)
            {
                List<FrameworkElement> frameworkElements;
                if (!RegisterAppShortcutKeysBehavior._associatedWindows.TryGetValue(window, out frameworkElements)) return;
                frameworkElements.Remove(commandTarget);
                if (frameworkElements.Count != 0) return;
                RegisterAppShortcutKeysBehavior._associatedWindows.Remove(window);
                window.PreviewKeyDown -= Window_PreviewKeyDown;
            }
        }

        /// <summary>
        ///     обработчик события перед нажатием клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool isShortcutKeystroke = (Keyboard.Modifiers != ModifierKeys.None) || RegisterAppShortcutKeysBehavior._shortcutKeys.Contains(e.Key);

            if (!isShortcutKeystroke) return;
            var window = sender as Window;
            if (window == null) return;
            // I hold the lock for the shortest time possible
            InputBinding[] inputBindings;
            lock (RegisterAppShortcutKeysBehavior._appInputBindingsLock)
            {
                inputBindings = RegisterAppShortcutKeysBehavior._readOnlyAppInputBindings;
            }

            foreach (InputBinding inputBinding in inputBindings)
            {
                if (!inputBinding.Gesture.Matches(inputBinding.CommandTarget, e)) continue;
                if (InvokeCommandIfTargetVisible(inputBinding))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        ///     Проверка на видимость элемента и если видимый, то вызов
        /// </summary>
        /// <param name="inputBinding"></param>
        /// <returns></returns>
        private static bool InvokeCommandIfTargetVisible(InputBinding inputBinding)
        {
            Debug.Assert(inputBinding != null);

            var frameworkElement = inputBinding.CommandTarget as FrameworkElement;

            // I only execute when the command target is visible.
            if ((frameworkElement != null) && (frameworkElement.IsVisible))
            {
                return InvokeCommand(inputBinding);
            }

            return false;
        }

        /// <summary>
        ///     Вызов команды
        /// </summary>
        /// <param name="inputBinding"></param>
        /// <returns></returns>
        private static bool InvokeCommand(InputBinding inputBinding)
        {
            Debug.Assert(inputBinding != null);

            // Routed commands are static, so we need to provide the command target
            // otherwise the default is to use the focused element.
            var routedCommand = inputBinding.Command as RoutedCommand;
            if (routedCommand != null)
            {
                if (!routedCommand.CanExecute(inputBinding.CommandParameter, inputBinding.CommandTarget)) return false;
                routedCommand.Execute(inputBinding.CommandParameter, inputBinding.CommandTarget);
                return true;
            }
            // Other commands are instance commands and can be called directly.
            if (!inputBinding.Command.CanExecute(inputBinding.CommandParameter)) return false;
            inputBinding.Command.Execute(inputBinding.CommandParameter);
            return true;
        }

        /// <summary>
        ///     Обработчик события для загрузки элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            RegisterAssociatedWindow(this._frameworkElement);
        }

        /// <summary>
        ///     Обработчик события для изменения контекста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this._frameworkElement == null) return;
            if (InputBindings != null)
            {
                RegisterBindings(InputBindings, this._frameworkElement);
            }
        }

        #endregion [Help members]
    }
}