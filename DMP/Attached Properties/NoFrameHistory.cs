namespace DMP {
    using System.Windows;
    using System.Windows.Navigation;
    using System.Windows.Controls;

    /// <inheritdoc/>
    /// <summary>
    /// The NoFrameHistory attached property for creating a <see cref="Frame"/> that never show
    /// navigation (or behaviour)
    /// </summary>
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        #region Public Methods

        /// <summary>
        /// What happends when the history of the frame changes?
        /// </summary>
        /// <param name="sender">The frame that has a changed content value</param>
        /// <param name="e">The event arguments passed along</param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var frame = (sender as Frame);

            // Hide the naviation bar
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            // Clear history on navigate
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }

        #endregion Public Methods
    }
}