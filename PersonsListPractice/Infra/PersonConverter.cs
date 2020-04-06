using System;
using System.Globalization;
using System.Windows.Data;

namespace PersonsListPractice.ViewModel
{
    public class PersonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int? person_id = (int?)values[0];
            string person_name = values[1] as string;
            if (string.IsNullOrEmpty(person_name) == false && person_id > 0)
                return true;
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}