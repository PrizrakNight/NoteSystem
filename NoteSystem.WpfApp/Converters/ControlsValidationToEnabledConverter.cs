using System;
using System.Globalization;
using System.Windows.Data;

namespace NoteSystem.WpfApp.Converters
{
    class ControlsValidationToEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var hasError = false;

            for (int i = 0; i < values.Length; i++)
                hasError |= (bool)values[i];

            return !hasError;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
