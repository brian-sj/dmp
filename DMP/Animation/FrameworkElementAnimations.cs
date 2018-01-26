namespace DMP.Animation
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Helpers to animate framework elements in specific ways
    /// </summary>
    public static class FrameworkElementAnimations
    {
        #region Slide from/to left

        /// <summary>
        /// Slide and fade in from left.
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same width during animation
        /// </param>
        /// <param name="width">Optional: Pass in a width of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeInFromLeftAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideFromLeft(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slide and fade out to Left
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same width during animation
        /// </param>
        /// <param name="width">Optional: Pass in a width of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeOutToLeftAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideToLeft(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        #endregion Slide from/to left

        #region Slide from/to right

        /// <summary>
        /// Slide and fade in from right.
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same width during animation
        /// </param>
        /// <param name="width">Optional: Pass in a width of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideFromRight(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slide and fade out to right.
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same width during animation
        /// </param>
        /// <param name="width">Optional: Pass in a width of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeOutToRightAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideToRight(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        #endregion Slide from/to right

        #region Slide from/to bottom

        /// <summary>
        /// Slide and fade in from bottom.
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same height during animation
        /// </param>
        /// <param name="height">Optional: Pass in a height of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeInFromBottomAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int height = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideFromBottom(seconds, height == 0 ? element.ActualWidth : height, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slide and fade out to bottom
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same height during animation
        /// </param>
        /// <param name="height">Optional: Pass in a height of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeOutToBottomAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int height = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideToBottom(seconds, height == 0 ? element.ActualWidth : height, keepMargin: keepMargin);
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        #endregion Slide from/to bottom

        #region Slide from/to top

        /// <summary>
        /// Slide and fade in from top.
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same height during animation
        /// </param>
        /// <param name="height">Optional: Pass in a height of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeInFromTopAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int height = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideFromTop(seconds, height == 0 ? element.ActualWidth : height, keepMargin: keepMargin);
            sb.AddFadeIn(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        /// <summary>
        /// Slide and fade out to top.
        /// </summary>
        /// <param name="element">The element to animate.</param>
        /// <param name="seconds">The seconds it takes to animate.</param>
        /// <param name="keepMargin">
        /// Optional: Wether to keep the element at the same height during animation
        /// </param>
        /// <param name="height">Optional: Pass in a height of the element to slide and fade in</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public static async Task SlideAndFadeOutToTopAsync(this FrameworkElement element, float seconds, bool keepMargin = true, int height = 0)
        {
            var sb = new Storyboard();
            sb.AddSlideToTop(seconds, height == 0 ? element.ActualWidth : height, keepMargin: keepMargin);
            sb.AddFadeOut(seconds);
            sb.Begin(element);
            element.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        #endregion Slide from/to top
    }
}