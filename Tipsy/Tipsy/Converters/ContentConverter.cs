// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>ContentConverter.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 4:06:33 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ContentConverter : IValueConverter
    {
        #region Creation

        public ContentConverter()
            : base()
        {
        }

        #endregion Creation

        #region IValueConverter Members

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            var result = value;
            if (result is string text)
            {
                var resource = App.Current.TryFindResource(text);
                if (resource is BitmapImage image)
                {
                    result = new Image { Source = image };
                }
            }
            return result;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        #endregion IValueConverter Members
    }
}
