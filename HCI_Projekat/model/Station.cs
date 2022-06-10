using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class Station : INotifyPropertyChanged, IComparable<Station>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public delegate void StationChangedHandler();
        public event StationChangedHandler StationChanged;

        protected void OnStationChanged()
        {
            StationChanged?.Invoke();
        }

        private string _name;

        public Station(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                    OnStationChanged();
                }
            }
        }

        public override string ToString()
        {
            return _name;
        }

        public int CompareTo(Station other)
        {
            if (other == null)
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
    }
}
