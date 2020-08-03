using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LotteryBook.Program.Converters
{
    public class NotNullToVisibilityConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var invertedParameter = parameter as string;
            var inverted = invertedParameter == "inverted";

            if (value == null && !inverted)
            {
                return Visibility.Collapsed;
            }

            if (value != null && inverted)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}