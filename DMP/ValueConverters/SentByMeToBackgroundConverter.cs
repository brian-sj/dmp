namespace DMP
{
    using System;
    using System.Globalization;
    using System.Windows;

    /// <inheritdoc/>
    /// <summary>
    /// A converter that takes in a boolean if a message was sent by me ( <see
    /// cref="Fasetto.Word.Core.ChatMessageListItemViewModel.SentByMe"/>), and returns the correct
    /// background color
    /// </summary>
    public class SentByMeToBackgroundConverter : BaseValueConverter<SentByMeToBackgroundConverter>
    {
        #region Public Methods

        /// <inheritdoc/>
        /// <summary>
        /// Convert a boolean to a static resource of either WordVeryLightBlueBrush or ForegroundLightBrush
        /// </summary>
        /// <param name="value">The boolean to evaluate</param>
        /// <param name="targetType">The type to convert to</param>
        /// <param name="parameter">Any parameters passed</param>
        /// <param name="culture">The language culture</param>
        /// <returns>The <see cref="T:System.Windows.Visibility"/>. value</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Application.Current.FindResource("WordVeryLightBlueBrush") : Application.Current.FindResource("ForegroundLightBrush");
        }

        /// <inheritdoc/>
        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="value">The <see cref="T:System.Windows.Visibility"/> value</param>
        /// <param name="targetType">The TargetType of the action to target</param>
        /// <param name="parameter">Any parameters passed</param>
        /// <param name="culture">The language culture</param>
        /// <returns>The <see cref="T:System.Boolean"/> value of this property</returns>
        /// <exception cref="T:System.NotImplementedException">
        /// We have not implemented this (yet)
        /// </exception>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}