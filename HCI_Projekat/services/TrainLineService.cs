using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.services
{
    public class TrainLineService
    {
        public static List<TrainLine> TrainLines { get; set; } = new List<TrainLine>();

        public TrainLineService()
        {
            string[] lessstations = { "Backa Topola", "Vrbas" };
            string[] morestations = { "Novi Zednik", "Backa Topola" , "Mali Idos", "Mali Idos Polje", "Vrbas", "Zmajevo", "Stepanovicevo", "Kisac"};
            TrainLine trainline1 = new TrainLine("SUNSS", "Subotica", "Novi Sad", lessstations);
            TrainLine trainline2 = new TrainLine("SUNSL","Subotica", "Novi Sad", morestations);
            TrainLines.Add(trainline1);
            TrainLines.Add(trainline2);
        }

        public SortedSet<Station> GetAllStations()
        {
            SortedSet<Station> allStations = new SortedSet<Station>();
            foreach (TrainLine trainLine in TrainLines)
            {
                allStations.Add(trainLine.DeparturePlace);
                allStations.Add(trainLine.ArrivalPlace);
                for (int i = 0; i < trainLine.Stations.Count; i++)
                {
                    allStations.Add(trainLine.Stations[i]);
                }
            }
            return allStations;
        }

        internal void RemoveTrainLine(TrainLine trainLine)
        {
            TrainLines.Remove(trainLine);
        }

        internal void AddTrainLine(TrainLine trainLine)
        {
            TrainLines.Add(trainLine);
        }
    }
}
