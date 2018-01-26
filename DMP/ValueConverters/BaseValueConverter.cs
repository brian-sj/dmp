// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseValueConverter.cs" company="">
//     2017 by AngelSix - modified by mitos[dash]kalandiel
// </copyright>
// <summary>
// A base value converter that allows direct XAML usage
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DMP
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// A base value converter that allows direct XAML usage
    /// </summary>
    /// <typeparam name="T">The type of the value converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private members

        /// <summary>
        /// A single static instance of the converter
        /// </summary>
        internal static T Converter { get; private set; } = null;

        #endregion Private members

        #region Markup Extension Methods

        /// <summary>
        /// Provides an instance of the value converter
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns>An instance of the converter</returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => Converter ?? (Converter = new T());

        #endregion Markup Extension Methods

        #region Value Converter Methods

        /// <inheritdoc/>
        /// <summary>
        /// The method to convert one type to another
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="targetType">The type to convert to</param>
        /// <param name="parameter">Any parameters passed into the converter</param>
        /// <param name="culture">The language culture (if any) to use</param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <inheritdoc/>
        /// <summary>
        /// The method to convert a value back to its origin type
        /// </summary>
        /// <param name="value">The value to convert back</param>
        /// <param name="targetType">The type to convert back to</param>
        /// <param name="parameter">Any parameters passed into the converter</param>
        /// <param name="culture">The language culture (if any) to use</param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion Value Converter Methods
    }
}