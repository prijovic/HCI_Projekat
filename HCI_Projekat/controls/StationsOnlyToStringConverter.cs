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
    [ValueConversion(typeof(ObservableCollection<Station>), typeof(string))]
    class StationsOnlyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<Station> stations = (ObservableCollection<Station>)value;
            string result = "";
            foreach (Station station in stations)
            {
                result += station.Name + "\n";
                result += "↓\n";
            }
            try
            {
                result = result.Remove(result.LastIndexOf('\n'));
                result = result.Remove(result.LastIndexOf('\n'));
            } catch(Exception)
            {
                return "Линија је директна, нема успутних станица.";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
