using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HCI_Projekat.controls
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            string date = $"{AddZero(dateTime.Day)}.{AddZero(dateTime.Month)}.{AddZero(dateTime.Year)}. {AddZero(dateTime.Hour)}:{AddZero(dateTime.Minute)}";
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.ParseExact((string)value, "dd.MM.yyyy. HH:mm", null);
        }

        private string AddZero(int number)
        {
            if (number < 10)
            {
                return $"0{number}";
            }
            return $"{number}";
        }
    }
}
