using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class StationArrival : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public delegate void StationArrivalChangedHandler();
        public event StationArrivalChangedHandler StationArrivalChanged;

        protected void OnStationArrivalChanged()
        {
            StationArrivalChanged?.Invoke();
        }

        public StationArrival(Station station, DateTime dateTime, double price)
        {
            Station = station;
            Time = dateTime;
            Price = price;
        }

        public StationArrival(Station station)
        {
            Station = station;
            Time = default(DateTime);
            Price = 0;
        }

        private Station _station;
        private DateTime _time;
        private double _price;

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                    OnStationArrivalChanged();
                }
            }
        }

        public Station Station
        {
            get
            {
                return _station;
            }
            set
            {
                if (value != _station)
                {
                    _station = value;
                    OnPropertyChanged("Station");
                    OnStationArrivalChanged();
                }
            }
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged("Time");
                    OnStationArrivalChanged();
                }
            }
        }
    }
}
