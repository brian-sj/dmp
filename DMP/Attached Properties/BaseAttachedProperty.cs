﻿namespace DMP
{ 
    using System;
    using System.Windows;

    /// <summary>
    /// A base attached property to replace the M$ way to do this
    /// </summary>
    /// <typeparam name="TParent">The parent class to the attached property</typeparam>
    /// <typeparam name="TProperty">The type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<TParent, TProperty>
        where TParent : new()
    {
        #region Public Properties

        /// <summary>
        /// Gets the singleton instance of the parent class
        /// </summary>
        public static TParent Instance { get; private set; } = new TParent();

        #endregion Public Properties

        #region Attached Property Definitions

        /// <summary>
        /// The attached property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(TProperty), typeof(BaseAttachedProperty<TParent, TProperty>), new UIPropertyMetadata(default(TProperty), new PropertyChangedCallback(OnValuePropertyChanged), new CoerceValueCallback(OnValuePropertyUpdated)));

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">The UI element that had its property changed</param>
        /// <param name="e">The arguments for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.OnValueChanged(d, e);

            // Call event listeners
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.ValueChanged(d, e);
        }

        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed, even it is the same value
        /// </summary>
        /// <param name="d">The UI element that had its property changed</param>
        /// <param name="value">The arguments for the event</param>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            //Call the parent function
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.OnValueUpdated(d, value);

            //Call the event listeners
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.ValueUpdated(d, value);

            //Return the value
            return value;
        }

        /// <summary>
        /// Gets the attached property value
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns>The value of the attached property</returns>
        public static TProperty GetValue(DependencyObject d) => (TProperty)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attached property
        /// </summary>
        /// <param name="d">The element to set the property for</param>
        /// <param name="value">The value to set the property to</param>
        public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);

        #endregion Attached Property Definitions

        #region Public events

        /// <summary>
        /// This fires when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        /// This fires when the value changes, even when the value is the same
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        #endregion Public events

        #region Event Methods

        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="e">The arguments for this event</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// The method that is called when any attached property of this type is changed, even if the
        /// value is the same
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="value">The arguments for this event</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value)
        {
        }

        #endregion Event Methods
    }
}