using System;
using System.Windows;
using System.Windows.Data;

namespace BackupManager.App.Converters
{
    public class BoolMultiConditionVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var condition1 = (bool)values[0];
            var condition2 = (bool)values[1];

            return condition1 && condition2 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}