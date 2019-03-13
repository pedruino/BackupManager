using System;
using System.Globalization;
using System.Windows.Data;

namespace BackupManager.App.Converters
{
    public class EnumToBooleanMutiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var checkValue = values[0];
            var property = values[1];

            if (checkValue is Enum thisEnum && property is Enum storedEnum)
            {
                return thisEnum.Equals(storedEnum);
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}