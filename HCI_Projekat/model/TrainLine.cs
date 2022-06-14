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
    public class TrainLine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _code;
        private Station _departurePlace;
        private Station _arrivalPlace;
        private ObservableCollection<Station> _lineStations;
        public List<Station> Stations { get; set; } = new List<Station>();

        internal Station GetStationByName(string name)
        {
            if (DeparturePlace.Name == name) { return DeparturePlace; }
            if (ArrivalPlace.Name == name) { return ArrivalPlace; }
            foreach (Station station in Stations)
            {
                if (station.Name == name)
                {
                    return station;
                }
            }
            return null;
        }

        public TrainLine(string code, string departurePlace, string arrivalPlace, string[] stations)
        {
            this._code = code;
            this._departurePlace = new Station(departurePlace);
            this._arrivalPlace = new Station(arrivalPlace);
            for (int i = 0; i < stations.Length; i++)
            {
                Station station = new Station(stations[i]);
                station.StationChanged += UpdateStations;
                Stations.Add(station);
            }
            LineStations = new ObservableCollection<Station>(Stations);
        }

        public ObservableCollection<Station> LineStations
        {
            get
            {
                return _lineStations;
            }
            set
            {
                if (value != _lineStations)
                {
                    _lineStations = value;
                    OnPropertyChanged("LineStations");
                }
            }
        }

        public void UpdateStations()
        {
            LineStations = new ObservableCollection<Station>(Stations);
        }

        public void RemoveStation(Station station)
        {
            Stations.Remove(station);
            UpdateStations();
        }

        public void AddStataion(string name)
        {
            Station station = new Station(name);
            station.StationChanged += UpdateStations;
            Stations.Add(station);
            UpdateStations();
        }

        public void MoveStation(Station station1, Station station2)
        {
            Stations.Remove(station1);
            int index = Stations.IndexOf(station2);
            Stations.Insert(index, station1);
            UpdateStations();
        }

        public Station DeparturePlace
        {
            get
            {
                return _departurePlace;
            }
            set
            {
                if (value != _departurePlace)
                {
                    _departurePlace = value;
                    UpdateStations();
                    OnPropertyChanged("DeparturePlace");
                }
            }
        }

        public Station ArrivalPlace
        {
            get
            {
                return _arrivalPlace;
            }
            set
            {
                if (value != _arrivalPlace)
                {
                    _arrivalPlace = value;
                    UpdateStations();
                    OnPropertyChanged("ArrivalPlace");
                }
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (value != _code)
                {
                    _code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        public override string ToString()
        {
            return $"{DeparturePlace}-{ArrivalPlace} ({Stations.Count})";
        }

        internal bool ContainsStation(Station station)
        {
            if (DeparturePlace.Name == station.Name) return true;
            if (ArrivalPlace.Name == station.Name) return true;
            foreach (Station s in Stations)
            {
                if (s.Name == station.Name)
                {
                    return true;
                }
            }
            return false;
        }

        internal bool IsStationAfter(Station s1, Station s2)
        {
            if (s1.Name == DeparturePlace.Name && s2.Name != s1.Name)
            {
                return true;
            }
            if (s2.Name ==ArrivalPlace.Name && s2.Name != s1.Name)
            {
                return true;
            }
            return GetIndexOfStationByName(s1) < GetIndexOfStationByName(s2);
        }

        internal int GetIndexOfStationByName(Station station)
        {
            foreach (Station s in Stations)
            {
               if (s.Name == station.Name)
                {
                    return Stations.IndexOf(s);
                }
            }
            return -1;
        }
    }
}
