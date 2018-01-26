namespace DMP
{
    using System;
    using System.Globalization;
    using System.Windows;

    /// <inheritdoc/>
    /// <summary>
    /// A converter that takes in a boolean if a message was sent by me ( <see
    /// cref="Fasetto.Word.Core.ChatMessageListItemViewModel.SentByMe"/>), and aligns to the right If
    /// not sent by me, aligns to the left
    /// </summary>
    public class SentByMeToAlignmentConverter : BaseValueConverter<SentByMeToAlignmentConverter>
    {
        #region Public Methods

        /// <inheritdoc/>
        /// <summary>
        /// Convert a boolean to either left or right alignment
        /// </summary>
        /// <param name="value">The boolean to evaluate</param>
        /// <param name="targetType">The type to convert to</param>
        /// <param name="parameter">Any parameters passed</param>
        /// <param name="culture">The language culture</param>
        /// <returns>The <see cref="T:System.Windows.Visibility"/>. value</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            else
                return (bool)value ? HorizontalAlignment.Left : HorizontalAlignment.Right;
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