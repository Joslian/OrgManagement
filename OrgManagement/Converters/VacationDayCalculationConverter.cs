using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using OrgManagement.Database.Models;

namespace OrgManagement.Converters
{
    public class VacationDayCalculationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime createdAt)
            {
                return Helpers.DateTimeConverter.CalcVacationDays(createdAt);
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
