namespace DMP
{
    using System;
    using System.Globalization;
    using System.Windows;

    /// <inheritdoc/>
    /// <summary>
    /// Convert a boolean to a <see cref="T:System.Windows.Visibility"/>.Hidden/.Visible property
    /// </summary>
    public class HorizontalAlignmentConverter : BaseValueConverter<HorizontalAlignmentConverter>
    {
        #region Public Methods

        /// <inheritdoc/>
        /// <summary>
        /// Convert an enum of <see cref="Core.ElementHorizontalAlignment"/> to a Horizontal Alignment
        /// </summary>
        /// <param name="value">The int value</param>
        /// <param name="targetType">What control to target</param>
        /// <param name="parameter">Any parameters passed</param>
        /// <param name="culture">The language culture</param>
        /// <returns>The <see cref="T:System.Windows.Visibility"/>. value</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (HorizontalAlignment)value;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Convert the <see cref="T:System.Windows.Visibility"/> back to a boolean value
        /// </summary>
        /// <param name="value">The <see cref="T:System.Windows.Visibility"/> value</param>
        /// <param name="targetType">The TargetType of the action to target</param>
        /// <param name="parameter">Any parameters passed</param>
        /// <param name="culture">The language culture</param>
        /// <returns>The <see cref="T:System.Boolean"/> value of this property</returns>
        /// <exception cref="T:System.NotImplementedException">
        /// We have not implemented this yet
        /// </exception>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        #endregion Public Methods
    }
}