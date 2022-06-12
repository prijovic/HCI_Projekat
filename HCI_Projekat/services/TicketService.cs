using HCI_Projekat.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.services
{
    public class TicketService
    {
        public static List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public TicketService()
        {
        }

        public SortedSet<Ticket> GetUsersTickets(string username)
        {
            SortedSet<Ticket> usersTickets = new SortedSet<Ticket>();
            foreach (Ticket ticket in Tickets)
            {
                if (ticket.Client.Username == username)
                {
                    usersTickets.Add(ticket);
                }
            }
            return usersTickets;
        }

        public static Dictionary<string, double> GetTicketGraphData()
        {
            Dictionary<string, double> graphData = new Dictionary<string, double>();
            for (int i = 0; i < 12; i++)
            {
                DateTime someDate = DateTime.Now.AddMonths(-i);
                graphData[TranslateMonth(someDate.Month)] = CountTicketsInMonth(someDate.Month, someDate.Year);
            }
            return graphData;
        }

        public static int CountTicketsInMonth(int month, int year)
        {
            int counter = 0;
            Tickets.ForEach(t =>
            {
                if (t.TimeStamp.Month == month && t.TimeStamp.Year == year)
                {
                    counter++;
                }
            });
            return counter;
        }

        private static string TranslateMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Јануар";
                case 2:
                    return "Фебруар";
                case 3:
                    return "Март";
                case 4:
                    return "Април";
                case 5:
                    return "Мај";
                case 6:
                    return "Јун";
                case 7:
                    return "Јул";
                case 8:
                    return "Август";
                case 9:
                    return "Септембар";
                case 10:
                    return "Октобар";
                case 11:
                    return "Новембар";
                case 12:
                    return "Децембар";
                default:
                    return null;
            }
        }

        internal static Dictionary<string, double> GetTicketLineGraphData(TrainLine trainLine)
        {
            Dictionary<string, double> graphData = new Dictionary<string, double>();
            for (int i = 0; i < 12; i++)
            {
                DateTime someDate = DateTime.Now.AddMonths(-i);
                graphData[TranslateMonth(someDate.Month)] = CountLineTicketsInMonth(trainLine, someDate.Month, someDate.Year);
            }
            return graphData;
        }

        public static int CountLineTicketsInMonth(TrainLine trainLine, int month, int year)
        {
            int counter = 0;
            Tickets.ForEach(t =>
            {
                if (t.TimeStamp.Month == month && t.TimeStamp.Year == year && t.ScheduleItem.TrainLine == trainLine)
                {
                    counter++;
                }
            });
            return counter;
        }
    }
}
