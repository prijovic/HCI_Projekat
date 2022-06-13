using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class SearchLine
    {
        public SearchLine(string departureStation, string arrivalStation)
        {
            DeparturePlace = new Station(departureStation);
            ArrivalPlace = new Station(arrivalStation);
        }

        public Station DeparturePlace { get; set; }
        public Station ArrivalPlace { get; set; }
    }
}
