// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanToVisibilityConverter.cs" company="mitos-kalandiel">
//     (c) 2017 by AngelSix - modified by mitos[dash]kalandiel
// </copyright>
// <summary>
// Convert a boolean to a <see cref="Visibility"/>.Hidden/.Visible property
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DMP
{
    using System;
    using System.Globalization;
    using System.Windows;

    /// <inheritdoc/>
    /// <summary>
    /// Convert a boolean to a <see cref="T:System.Windows.Visibility"/>.Hidden/.Visible property
    /// </summary>
    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
    {
        #region Public Methods

        /// <inheritdoc/>
        /// <summary>
        /// Convert false to hidden and true to visible
        /// </summary>
        /// <param name="value">The boolean value</param>
        /// <param name="targetType">What control to target</param>
        /// <param name="parameter">Any parameters passed</param>
        /// <param name="culture">The language culture</param>
        /// <returns>The <see cref="T:System.Windows.Visibility"/>. value</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // In case we don't pass a parameter (like we do with the Animation objects) we define
            // true as hidden whereas with all other cases where we pass a parameter we do the opposite
            if (parameter == null)
            {
                return value != null && (bool)value ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                return value != null && (bool)value ? Visibility.Visible : Visibility.Hidden;
            }
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
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}