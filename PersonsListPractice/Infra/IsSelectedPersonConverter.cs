using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PersonsListPractice.ViewModel
{
    public class IsSelectedPersonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var PlistSelected = ((ObservableCollection<PersonVM>)value).Where(p => p.IsSelected == true);
            if (PlistSelected.Any() == false)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}