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
    public class ScheduleItem : INotifyPropertyChanged, IComparable<ScheduleItem>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private TrainLine _trainLine;
        private DateTime _departureTime;

        internal Station GetStationByName(string name)
        {
            return TrainLine.GetStationByName(name);
        }

        internal DateTime GetDepartureTimeByStationName(string name)
        {
            Station station = TrainLine.GetStationByName(name);
            if (TrainLine.DeparturePlace.Name == name) { return DepartureTime; }
            if (TrainLine.ArrivalPlace.Name == name) { return ArrivalTime; }
            return (StationArrivals.ElementAt(TrainLine.GetIndexOfStationByName(station))).Time;
        }

        internal double GetPrice(Station departurePlace, Station arrivalPlace)
        {
            return GetPriceByStation(arrivalPlace) - GetPriceByStation(departurePlace);
        }

        internal double GetPriceByStation(Station station)
        {
            if (TrainLine.DeparturePlace == station) { return 0; }
            if (TrainLine.ArrivalPlace == station) { return Price; }
            foreach (StationArrival stationArrival in StationArrivals)
            {
                if (stationArrival.Station == station)
                {
                    return stationArrival.Price;
                }
            }
            return 0;
        }

        internal TimeSpan GetTripDuration(Station departurePlace, Station arrivalPlace)
        {
            return GetDepartureTimeByStationName(arrivalPlace.Name).Subtract(GetDepartureTimeByStationName(departurePlace.Name));
        }

        private DateTime _arrivalTime;
        private Train _train;
        private double _price;
        private double _stationPrice;
        private DateTime _stationTime;
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

        public int CompareTo(ScheduleItem other)
        {
            if (other == null)
            {
                return 1;
            }
            return DepartureTime.CompareTo(other.DepartureTime);
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

        public double StationPrice
        {
            get
            {
                return _stationPrice;
            }
            set
            {
                if(value != _stationPrice)
                {
                    _stationPrice = value;
                    OnPropertyChanged("StationPrice");
                }
            }
        }

        public DateTime StationTime
        {
            get
            {
                return _stationTime;
            }
            set
            {
                if(value != _stationTime)
                {
                    _stationTime = value;
                    OnPropertyChanged("StationTime");
                }
            }
        }

        public bool ConstainsStation(Station station)
        {
            return TrainLine.ContainsStation(station);
        }

        public bool IsStationAfter(Station s1, Station s2)
        {
            return TrainLine.IsStationAfter(s1, s2);
        }

    }
}
