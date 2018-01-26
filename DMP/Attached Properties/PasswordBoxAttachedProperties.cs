namespace DMP
{
    using System.Windows;
    using System.Windows.Controls;

    /// <inheritdoc/>
    /// <summary>
    /// The HasText attached property for a <see cref="T:System.Windows.Controls.PasswordBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        #region Public Methods

        /// <summary>
        /// Set the HasText property based on if the caller <see cref="PasswordBox"/> has any text
        /// </summary>
        /// <param name="sender">The passsword box that we are monitoring</param>
        public static void SetValue(DependencyObject sender)
        {
            SetValue(sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }

        #endregion Public Methods
    }

    /// <inheritdoc/>
    /// <summary>
    /// The MonitorPassword attached property for a <see cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        #region Private Methods

        /// <summary>
        /// Fire when the <see cref="PasswordBox"/> value changes
        /// </summary>
        /// <param name="sender">The attached <see cref="PasswordBox"/></param>
        /// <param name="e">The event arguments</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Set the attached HasText value
            HasTextProperty.SetValue((PasswordBox)sender);
        }

        #endregion Private Methods

        /// <summary>
        /// Override the OnValueChanged base function to check if the <see cref="PasswordBox"/> text
        /// has changed
        /// </summary>
        /// <param name="sender">The PasswordBox to check</param>
        /// <param name="e">The event arguments</param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller
            var passwordBox = (PasswordBox)sender;

            // Make sure we are using a PasswordBox
            if (passwordBox == null)
            {
                return;
            }

            // Remove any previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // If the caller set MonitorPassword to true, start listening for an event
            if ((bool)e.NewValue)
            {
                // Set default value
                HasTextProperty.SetValue(passwordBox);

                // Start listening for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }
    }
}