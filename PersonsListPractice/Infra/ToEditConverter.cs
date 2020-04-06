using System;
using System.Globalization;
using System.Windows.Data;

namespace PersonsListPractice.ViewModel
{
    public class ToEditConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool toEdit = (bool)value;
            if (toEdit)
                return "Visible";
            return "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}