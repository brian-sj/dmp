namespace DMP
{
    using System.Windows;
    using System.Windows.Controls;

    /// <inheritdoc/>
    /// <summary>
    /// Focuses this element on load
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        #region Public Methods

        /// <summary>
        /// Override normal attached property method And replace it with a function where we focus on
        /// an element (if it is a control)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If we are not a control, simply return
            if (!(sender is Control control))
                return;

            //Focus this control once its loaded.
            control.Loaded += (s, se) => control.Focus();
        }

        #endregion Public Methods
    }
}