using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class Ticket : INotifyPropertyChanged, IComparable<Ticket>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int CompareTo(Ticket other)
        {
            if (other == null)
            {
                return 1;
            }
            return TimeStamp.CompareTo(other.TimeStamp);
        }

        public Ticket(User client, DateTime timeStamp, ScheduleItem scheduleItem, Station departurePlace, Station arrivalPlace, bool paid)
        {
            _client = client;
            _timeStamp = timeStamp;
            _scheduleItem = scheduleItem;
            _paid = paid;
            _arrivalPlace = arrivalPlace;
            _departurePlace = departurePlace;
        }

        public Ticket(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }

        private User _client;
        private DateTime _timeStamp;
        private ScheduleItem _scheduleItem;
        private Station _departurePlace;
        private Station _arrivalPlace;
        private bool _paid;

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
                    OnPropertyChanged("ArrivalPlace");
                }
            }
        }

        public User Client
        {
            get
            {
                return _client;
            }
            set
            {
                if (value != _client)
                {
                    _client = value;
                    OnPropertyChanged("Client");
                }
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
            set
            {
                if (value != _timeStamp)
                {
                    _timeStamp = value;
                    OnPropertyChanged("TimeStamp");
                }
            }
        }

        public ScheduleItem ScheduleItem
        {
            get
            {
                return _scheduleItem;
            }
            set
            {
                if (value != _scheduleItem)
                {
                    _scheduleItem = value;
                    OnPropertyChanged("ScheduleItem");
                }
            }
        }

        public bool Paid
        {
            get
            {
                return _paid;
            }
            set
            {
                if (value != _paid)
                {
                    _paid = value;
                    OnPropertyChanged("Paid");
                }
            }
        }
    }
}
