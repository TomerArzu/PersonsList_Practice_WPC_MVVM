using System;
using System.Globalization;
using System.Windows.Data;

namespace PersonsListPractice.ViewModel
{
    public class IdExistConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int?)value == null)
                return "Visible";
            return "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}