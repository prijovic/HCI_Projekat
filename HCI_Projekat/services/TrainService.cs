using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.services
{
    public class TrainService
    {
        public static List<Train> Trains { get; set; } = new List<Train>();

        public TrainService()
        {
            Train train1 = new Train("Soko", "AC123", 300);
            Train train2 = new Train("Cira", "AB123", 350);
            Train train3 = new Train("Brzi", "AD123", 200);
            Train train4 = new Train("Regio", "FB123", 250);
            Train train5 = new Train("Spori", "4B123", 300);
            Train train6 = new Train("Teretni", "HB123", 10);
            Train train7 = new Train("Soko1", "HB623", 100);
            Train train8 = new Train("Soko2", "AB1523", 300);
            Trains.Add(train1);
            Trains.Add(train2);
            Trains.Add(train3);
            Trains.Add(train4);
            Trains.Add(train5);
            Trains.Add(train6);
            Trains.Add(train7);
            Trains.Add(train8);
        }

        public void RemoveTrain(Train train)
        {
            Trains.Remove(train);
        }

        public void AddTrain(Train train)
        {
            Trains.Add(train);
        }
    }
}
