using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCI_Projekat.model
{
    public class ScheduleItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private TrainLine _trainLine;
        private DateTime _departureTime;
        private DateTime _arrivalTime;
        private Train _train;
        private double _price;
        public ObservableCollection<StationArrival> _arrivals;
        public List<StationArrival> StationArrivals { get; set; } = new List<StationArrival>();

        public ScheduleItem(TrainLine trainLine, DateTime departureTime, DateTime arrivalTime, DateTime[] stations, Train train, double price, double[] prices)
        {
            TrainLine = trainLine;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Train = train;
            Price = price;
            for (int i = 0; i < stations.Length; i++)
            {
                StationArrival stationArrival = new StationArrival(trainLine.Stations[i], stations[i], prices[i]);
                stationArrival.StationArrivalChanged += UpdateArrivals;
                StationArrivals.Add(stationArrival);
            }
            Arrivals = new ObservableCollection<StationArrival>(StationArrivals);
        }

        public TimeSpan TripDuration
        {
            get
            {
                return ArrivalTime.Subtract(DepartureTime);
            }
            set
            {

            }
        }

        public void UpdateArrivals()
        {
            Arrivals = new ObservableCollection<StationArrival>(StationArrivals);
        }

        public ObservableCollection<StationArrival> Arrivals
        {
            get
            {
                return _arrivals;
            }
            set
            {
                if (value != _arrivals)
                {
                    _arrivals = value;
                    OnPropertyChanged("Arrivals");
                }
            }
        }

        public TrainLine TrainLine
        {
            get
            {
                return _trainLine;
            }
            set
            {
                if (value != _trainLine)
                {
                    _trainLine = value;
                    OnPropertyChanged("TrainLine");
                }
            }
        }

        public DateTime DepartureTime
        {
            get
            {
                return _departureTime;
            }
            set
            {
                if (value != _departureTime)
                {
                    _departureTime = value;
                    OnPropertyChanged("DepartureTime");
                }
            }
        }

        public DateTime ArrivalTime
        {
            get
            {
                return _arrivalTime;
            }
            set
            {
                if (value != _arrivalTime)
                {
                    _arrivalTime = value;
                    OnPropertyChanged("ArrivalTime");
                }
            }
        }

        public Train Train
        {
            get
            {
                return _train;
            }
            set
            {
                if (value != _train)
                {
                    _train = value;
                    OnPropertyChanged("Train");
                }
            }
        }

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
                }
            }
        }

    }
}
