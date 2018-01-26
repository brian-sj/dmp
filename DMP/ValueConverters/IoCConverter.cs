

namespace DMP
{
    //using Ninject;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    
/*
    /// <summary>
    /// Convert a string name to a service pulled from the IoC container
    /// </summary>
    public class IoCConverter : BaseValueConverter<IoCConverter>
    {
        #region Public Methods

        /// <inheritdoc/>
        /// <summary>
        /// Convert an <see cref="T:Fasetto.Word.ApplicationPage"/> to an <see
        /// cref="T:System.Object"/> containing the actual PageValue
        /// </summary>
        /// <param name="value">The <see cref="T:Fasetto.Word.ApplicationPage"/> value to convert</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> to convert to</param>
        /// <param name="parameter">Any parameter passed through</param>
        /// <param name="culture">The language <see cref="T:System.Globalization.CultureInfo"/>.</param>
        /// <returns>The <see cref="T:System.Object"/> containing an actual page.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                switch ((string)parameter)
                {
                    case nameof(ApplicationViewModel):
                        return IoC.Get<ApplicationViewModel>();

                    default:
                        Debugger.Break();
                        return null;
                }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// (Not implemented!) Convert the page back to an <see cref="ApplicationPage"/>.
        /// </summary>
        /// <param name="value">The page to convert back.</param>
        /// <param name="targetType">The <see cref="Type"/> to convert back to</param>
        /// <param name="parameter">Any parameter passed through</param>
        /// <param name="culture">The language <see cref="CultureInfo"/>.</param>
        /// <returns>An <see cref="ApplicationPage"/> value.</returns>
        /// <exception cref="NotImplementedException">
        /// We don't need this function, thus its not implemented.
        /// </exception>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
*/
}