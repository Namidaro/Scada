using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Controls
{
    /// <summary>
    ///     Represents a progress ring control
    ///     Представляет контрол прогресса.(Вьюшка выводящаяся при загрузке)
    /// </summary>
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Small", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class ProgressRing : Control
    {
        #region [Dependency properties metadata]

        /// <summary>
        ///     Gets BindableWidthProperty metadata
        /// </summary>
        public static readonly DependencyProperty BindableWidthProperty = DependencyProperty.Register("BindableWidth",
            typeof (double),
            typeof (
                ProgressRing),
            new PropertyMetadata
                (default(
                    double),
                    BindableWidthCallback));

        /// <summary>
        ///     Gets IsActiveProperty metadata
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive",
            typeof (bool),
            typeof (ProgressRing),
            new FrameworkPropertyMetadata
                (default(bool),
                    FrameworkPropertyMetadataOptions
                        .BindsTwoWayByDefault,
                    IsActiveChanged));

        /// <summary>
        ///     Gets IsLargeProperty metadata
        /// </summary>
        public static readonly DependencyProperty IsLargeProperty = DependencyProperty.Register("IsLarge", typeof (bool),
            typeof (ProgressRing),
            new PropertyMetadata(
                true,
                IsLargeChangedCallback));

        /// <summary>
        ///     gets MaxSideLengthProperty metadata
        /// </summary>
        public static readonly DependencyProperty MaxSideLengthProperty = DependencyProperty.Register("MaxSideLength",
            typeof (double),
            typeof (
                ProgressRing),
            new PropertyMetadata
                (default(
                    double)));

        /// <summary>
        ///     Gets EllipseDiameterProperty metadata
        /// </summary>
        public static readonly DependencyProperty EllipseDiameterProperty =
            DependencyProperty.Register("EllipseDiameter", typeof (double), typeof (ProgressRing),
                new PropertyMetadata(default(double)));

        /// <summary>
        ///     Gets EllipseOffsetProperty metadata
        /// </summary>
        public static readonly DependencyProperty EllipseOffsetProperty = DependencyProperty.Register("EllipseOffset",
            typeof (Thickness),
            typeof (
                ProgressRing),
            new PropertyMetadata
                (default(
                    Thickness
                    )));

        #endregion [Dependency properties metadata]

        #region [Private fields]

        private List<Action> _deferredActions = new List<Action>();

        #endregion [Private fields]

        #region [Ctor's]

        static ProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ProgressRing),
                new FrameworkPropertyMetadata(typeof (ProgressRing)));
        }

        /// <summary>
        ///     Creates an instance of <see cref="ProgressRing" />
        /// </summary>
        public ProgressRing()
        {
            SizeChanged += OnSizeChanged;
        }

        #endregion [Ctor's]

        #region [Properties]

        /// <summary>
        ///     Gets or sets the maximum side lkingth
        /// </summary>
        public double MaxSideLength
        {
            get { return (double) GetValue(MaxSideLengthProperty); }
            private set { SetValue(MaxSideLengthProperty, value); }
        }

        /// <summary>
        ///     Gets or sets ellipse diameter
        /// </summary>
        public double EllipseDiameter
        {
            get { return (double) GetValue(EllipseDiameterProperty); }
            private set { SetValue(EllipseDiameterProperty, value); }
        }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Thickness" /> that represents Ellipse offset
        /// </summary>
        public Thickness EllipseOffset
        {
            get { return (Thickness) GetValue(EllipseOffsetProperty); }
            private set { SetValue(EllipseOffsetProperty, value); }
        }

        /// <summary>
        ///     Gets or sets bindable width
        /// </summary>
        public double BindableWidth
        {
            get { return (double) GetValue(BindableWidthProperty); }
            private set { SetValue(BindableWidthProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value that indicates whether current control is active or not
        /// </summary>
        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value that indicates whether the control is large
        /// </summary>
        public bool IsLarge
        {
            get { return (bool) GetValue(IsLargeProperty); }
            set { SetValue(IsLargeProperty, value); }
        }

        #endregion [Properties]

        #region [Override members]

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal processes call
        ///     <see
        ///         cref="M:System.Windows.FrameworkElement.ApplyTemplate" />
        ///     .
        /// </summary>
        public override void OnApplyTemplate()
        {
            //make sure the states get updated
            UpdateLargeState();
            UpdateActiveState();
            base.OnApplyTemplate();
            if (this._deferredActions != null)
                foreach (Action action in this._deferredActions)
                    action();
            this._deferredActions = null;
        }

        #endregion [Override members]

        #region [Help members]

        private static void BindableWidthCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ProgressRing;
            if (ring == null)
                return;

            var action = new Action(() =>
            {
                ring.SetEllipseDiameter(
                    (double) dependencyPropertyChangedEventArgs.NewValue);
                ring.SetEllipseOffset(
                    (double) dependencyPropertyChangedEventArgs.NewValue);
                ring.SetMaxSideLength(
                    (double) dependencyPropertyChangedEventArgs.NewValue);
            });

            if (ring._deferredActions != null)
                ring._deferredActions.Add(action);
            else
                action();
        }

        private void SetMaxSideLength(double width)
        {
            this.MaxSideLength = width <= 20 ? 20 : width;
        }

        private void SetEllipseDiameter(double width)
        {
            this.EllipseDiameter = width/8;
        }


        private void SetEllipseOffset(double width)
        {
            this.EllipseOffset = new Thickness(0, width/2, 0, 0);
        }

        private static void IsLargeChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ProgressRing;
            if (ring == null)
                return;

            ring.UpdateLargeState();
        }

        private void UpdateLargeState()
        {
            Action action;

            if (IsLarge)
                action = () => VisualStateManager.GoToState(this, "Large", true);
            else
                action = () => VisualStateManager.GoToState(this, "Small", true);

            if (this._deferredActions != null)
                this._deferredActions.Add(action);

            else
                action();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            this.BindableWidth = ActualWidth;
        }

        private static void IsActiveChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as ProgressRing;
            if (ring == null)
                return;

            ring.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            Action action;

            if (IsActive)
                action = () => VisualStateManager.GoToState(this, "Active", true);
            else
                action = () => VisualStateManager.GoToState(this, "Inactive", true);

            if (this._deferredActions != null)
                this._deferredActions.Add(action);

            else
                action();
        }

        #endregion [Help members]
    }

    internal class WidthToMaxSideLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var width = (double) value;
                return width <= 20 ? 20 : width;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}