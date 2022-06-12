using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.services
{
    public class ScheduleItemService
    {
        public static List<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();

        public ScheduleItemService()
        {
            DateTime[] arrivals = { DateTime.Now.AddMinutes(30), DateTime.Now.AddMinutes(45) };
            double[] prices = { 450, 750 };
            ScheduleItem scheduleItem1 = new ScheduleItem(TrainLineService.TrainLines[0], DateTime.Now, DateTime.Now.AddHours(1), arrivals, TrainService.Trains[2], 900, prices);
            ScheduleItems.Add(scheduleItem1);
        }

        public SortedSet<ScheduleItem> GetSchedules(SearchLine searchLine, DateTime date)
        {
            SortedSet<ScheduleItem> schedule = new SortedSet<ScheduleItem>();
            foreach (ScheduleItem si in ScheduleItems)
            {
                if (si.DepartureTime.Date == date.Date)
                {
                    if (si.ConstainsStation(searchLine.DeparturePlace) && si.ConstainsStation(searchLine.ArrivalPlace))
                    {
                        if (si.IsStationAfter(searchLine.DeparturePlace, searchLine.ArrivalPlace))
                        {
                            schedule.Add(si);
                        }
                    }
                }
            }
            return schedule;
        }

        public void RemoveScheduleItem(ScheduleItem scheduleItem)
        {
            ScheduleItems.Remove(scheduleItem);
        }

        internal DateTime GetTime(Station station)
        {
            foreach (ScheduleItem si in ScheduleItems)
            {
                foreach (StationArrival stationArrival in si.StationArrivals)
                {
                    if (stationArrival.Station == station)
                    {
                        return stationArrival.Time;
                    }
                }
            }
            return default(DateTime);
        }

        internal void SetTime(Station station, DateTime dateTime)
        {
            foreach (ScheduleItem si in ScheduleItems)
            {
                foreach (StationArrival stationArrival in si.StationArrivals)
                {
                    if (stationArrival.Station == station)
                    {
                        stationArrival.Time = dateTime;
                    }
                }
            }
        }

        internal void SetPrice(Station station, double price)
        {
            foreach (ScheduleItem si in ScheduleItems)
            {
                foreach (StationArrival stationArrival in si.StationArrivals)
                {
                    if (stationArrival.Station == station)
                    {
                        stationArrival.Price = price;
                    }
                }
            }
        }

        internal double GetPrice(Station station)
        {
            foreach (ScheduleItem si in ScheduleItems)
            {
                foreach (StationArrival stationArrival in si.StationArrivals)
                {
                    if (stationArrival.Station == station)
                    {
                        return stationArrival.Price;
                    }
                }
            }
            return 0;
        }

        internal void AddScheduleItem(ScheduleItem scheduleItem)
        {
            ScheduleItems.Add(scheduleItem);
        }
    }
}
