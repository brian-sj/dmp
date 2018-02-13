using System;
using System.Globalization;
using System.Windows;

namespace DMP
{
    class StringParamToVisibilityConverter : BaseValueConverter<StringParamToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return Visibility.Collapsed;
            else
            {
                Visibility i = (int)value == (int)parameter ? Visibility.Visible : System.Windows.Visibility.Collapsed;
                return (int)value == (int)parameter ? Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
                
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
    }
}
