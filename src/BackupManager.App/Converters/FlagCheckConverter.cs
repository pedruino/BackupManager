using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BackupManager.App.Converters
{
    public class FlagCheckConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var checkValue = values[0];
            var flags = values[1];


            if (checkValue is Enum @enum && flags is Enum enum1)
            {
                return enum1.HasFlag(@enum);
            }
            return (object)false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}