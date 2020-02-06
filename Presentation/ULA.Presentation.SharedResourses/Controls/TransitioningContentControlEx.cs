using System;
using System.Reflection;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

namespace ULA.Presentation.SharedResourses.Controls
{
    /// <summary>
    ///     Represents a transitioning content control that fixes data template selector issue in
    ///     <see cref="TransitioningContentControl" />
    /// </summary>
    public class TransitioningContentControlEx : TransitioningContentControl
    {
        #region [Fields]

        private static readonly Func<TransitioningContentControlEx, ContentPresenter>
            _currentContentPresentationSiteGetter;

        private static readonly Func<TransitioningContentControlEx, ContentPresenter>
            _previousContentPresentationSiteGetter;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Initializes <see cref="TransitioningContentControlEx" /> type
        /// </summary>
        static TransitioningContentControlEx()
        {
            TransitioningContentControlEx._previousContentPresentationSiteGetter = CreateGetter("PreviousContentPresentationSite");
            TransitioningContentControlEx._currentContentPresentationSiteGetter = CreateGetter("CurrentContentPresentationSite");
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Called when the value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property changes.
        /// </summary>
        /// <param name="oldContent">The old value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property.</param>
        /// <param name="newContent">The new value of the <see cref="P:System.Windows.Controls.ContentControl.Content" /> property.</param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            ContentPresenter currentContentPresentationSite = TransitioningContentControlEx._currentContentPresentationSiteGetter(this);
            if (ContentTemplateSelector != null)
            {
                ContentPresenter previousContentPresentationSite = TransitioningContentControlEx._previousContentPresentationSiteGetter(this);

                if (previousContentPresentationSite == null || currentContentPresentationSite == null) return;
                previousContentPresentationSite.ContentTemplate = ContentTemplateSelector.SelectTemplate(
                    oldContent, this);
                currentContentPresentationSite.ContentTemplate = ContentTemplateSelector.SelectTemplate(newContent, this);
            }
            base.OnContentChanged(oldContent, newContent);
        }

        #endregion [Override members]

        #region [Help members]

        private static Func<TransitioningContentControlEx, ContentPresenter> CreateGetter(string propertyName)
        {
            PropertyInfo property = typeof (TransitioningContentControl).GetProperty(propertyName,
                BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo getMethod = property.GetGetMethod(true);
            return (Func<TransitioningContentControlEx, ContentPresenter>)
                Delegate.CreateDelegate(typeof (Func<TransitioningContentControlEx, ContentPresenter>), getMethod);
        }

        #endregion [Help members]
    }
}