using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class SearchResultItem
    {
        public Station DeparturePlace { get; set; }
        public Station ArrivalPlace { get; set; }
        public DateTime DepartureTime { get; set; }
        public TimeSpan TripDuration { get; set; }
        public double Price { get; set; }
        public ScheduleItem ScheduleItem { get; set; }
        public ObservableCollection<StationArrival> StationArrivals { get; set; }

        public SearchResultItem(ScheduleItem scheduleItem, SearchLine searchLine)
        {
            DeparturePlace = scheduleItem.GetStationByName(searchLine.DeparturePlace.Name);
            ArrivalPlace = scheduleItem.GetStationByName(searchLine.ArrivalPlace.Name);
            DepartureTime = scheduleItem.GetDepartureTimeByStationName(searchLine.DeparturePlace.Name);
            TripDuration = scheduleItem.GetTripDuration(DeparturePlace, ArrivalPlace);
            Price = scheduleItem.GetPrice(DeparturePlace, ArrivalPlace);
            ScheduleItem = scheduleItem;
            StationArrivals = new ObservableCollection<StationArrival>(ScheduleItem.StationArrivals);
            StationArrivals.Insert(0, new StationArrival(scheduleItem.TrainLine.DeparturePlace, DepartureTime, 0));
            StationArrivals.Add(new StationArrival(scheduleItem.TrainLine.ArrivalPlace, scheduleItem.ArrivalTime, scheduleItem.Price));
        }
    }
}
