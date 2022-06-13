using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HCI_Projekat.controls
{
    [ValueConversion(typeof(Train), typeof(string))]
    class TrainToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Train train = (Train)value;
            return train.ToString() + $"(капацитет: {train.Capacity})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
