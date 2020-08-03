using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace LotteryBook.Program.Converters
{
    public abstract class BoolConverter<T> : IValueConverter
    {
        public BoolConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }

        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && ((bool)value))
            {
                return True;
            }

            return False;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }
}