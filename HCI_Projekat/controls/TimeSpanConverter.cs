using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HCI_Projekat.controls
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan timeSpan = (TimeSpan)value;
            return $"{(timeSpan.Hours > 0 ? timeSpan.Hours + " " + HourForm(timeSpan.Hours) + " " : "")}{ (timeSpan.Minutes > 0 ? timeSpan.Minutes +" мин":"")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string init = (string)value;
            string[] parts = init.Split(' ');
            TimeSpan result = new TimeSpan(int.Parse(parts[0]), int.Parse(parts[2]), 0);
            return result;
        }

        public string HourForm(int hours)
        {
            if (hours == 1)
            {
                return "сат";
            }
            else if (hours > 1 && hours < 5)
            {
                return "сата";
            }
            else
            {
                return "сати";
            }
        }
    }
}
