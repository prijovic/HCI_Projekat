using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HCI_Projekat.controls
{
    [ValueConversion(typeof(ObservableCollection<StationArrival>), typeof(string))]
    class StationsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<StationArrival> stations = (ObservableCollection<StationArrival>)value;
            string result = "";
            foreach (StationArrival station in stations) {
                DateTime dateTime = station.Time;
                result += $"{AddZero(dateTime.Day)}.{AddZero(dateTime.Month)}.{AddZero(dateTime.Year)}. {AddZero(dateTime.Hour)}:{AddZero(dateTime.Minute)}";
                result += " • ";
                result += station.Station.Name + (station.Price != 0? $"({station.Price} РСД)": "")  + "\n";
                result += "                               ↓\n";
            }
            result = result.Remove(result.LastIndexOf('\n'));
            result = result.Remove(result.LastIndexOf('\n'));
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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
