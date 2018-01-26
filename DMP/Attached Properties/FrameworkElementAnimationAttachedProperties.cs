using DMP.Animation;
using System.Windows;

namespace DMP
{
    /// <inheritdoc/>
    /// <summary>
    /// A base class to run any animation method when a bool is set to true and a reverse animation
    /// set to false
    /// </summary>
    /// <typeparam name="TParent">Parent class to inherit from</typeparam>
    public abstract class AnimateBaseProperty<TParent> : BaseAttachedProperty<TParent, bool>
        where TParent : BaseAttachedProperty<TParent, bool>, new()
    {
        #region Public Properties

        /// <summary>
        /// Check whether this is the first time property has been loaded.
        /// </summary>
        public bool FirstLoad { get; set; } = true;

        #endregion Public Properties


        #region Protected Methods

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The Framework element</param>
        /// <param name="value">The value</param>
        protected virtual void DoAnimationAsync(FrameworkElement element, bool value)
        {
        }

        #endregion Protected Methods

        /// <inheritdoc/>
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            //Get the framework element
            if (!(sender is FrameworkElement element))
                return;

            // Don't fire if the value doesn't change
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            // On first load ...
            if (FirstLoad)
            {
                // Create a single self-unhookable event for the elements loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // Unhook ourselves
                    element.Loaded -= onLoaded;

                    // Do the desired animation
                    DoAnimationAsync(element, (bool)value);

                    // No longer in first load
                    FirstLoad = false;
                };
                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }
            else
            {
                // Do the desired animation
                DoAnimationAsync(element, (bool)value);
            }
        }
    }

    /// <summary>
    /// Animates a framework element sliding it in from the bottom on show and sliding it out to the
    /// bottom on hide
    /// </summary>
    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }

        #endregion Protected Methods
    }

    /// <summary>
    /// Animates a framework element sliding it in from the left on show and sliding it out to the
    /// left on hide
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }

        #endregion Protected Methods
    }

    /// <summary>
    /// Animates a framework element sliding it in from the left on show and sliding it out to the
    /// left on hide
    /// </summary>
    public class AnimateSlideInFromRightProperty : AnimateBaseProperty<AnimateSlideInFromRightProperty>
    {
        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeInFromRightAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                await element.SlideAndFadeOutToRightAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }

        #endregion Protected Methods
    }

    /// <summary>
    /// Animates a framework element sliding it in from the bottom on show and sliding it out to the
    /// bottom on hide
    /// </summary>
    public class AnimateSlideInFromTopProperty : AnimateBaseProperty<AnimateSlideInFromTopProperty>
    {
        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        protected override async void DoAnimationAsync(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeInFromTopAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else
            {
                await element.SlideAndFadeOutToTopAsync(FirstLoad ? 0 : 0.3f, false);
            }
        }

        #endregion Protected Methods
    }
}