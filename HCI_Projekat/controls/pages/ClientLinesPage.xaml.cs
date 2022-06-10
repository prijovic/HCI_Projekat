using HCI_Projekat.model;
using HCI_Projekat.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat.controls.pages
{
    /// <summary>
    /// Interaction logic for ClientMainPage.xaml
    /// </summary>
    public partial class ClientLinesPage : Page
    {
        DateTime today = DateTime.Now;
        DateTime oneYearAhead = DateTime.Now.AddYears(1);
        TrainLineService trainLineService;
        SortedSet<string> stations = new SortedSet<string>();

        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }

        public ClientLinesPage(ref TrainLineService trainLineService)
        {
            this.trainLineService = trainLineService;
            DepartureDate = today;
            InitializeComponent();
            this.DataContext = this;
            datepicker.BlackoutDates.AddDatesInPast();
            datepicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddYears(1).AddDays(1), DateTime.Now.AddYears(100)));
            foreach (Station station in trainLineService.GetAllStations())
            {
                departurePlace.Items.Add(station.Name);
                arrivalPlace.Items.Add(station.Name);
            }
            departurePlace.SelectedItem = null;
            arrivalPlace.SelectedItem = null;
            clearButton.IsEnabled = false;
            searchButton.IsEnabled = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            departurePlace.SelectedItem = null;
            arrivalPlace.SelectedItem = null;
            DepartureDate = today;
            clearButton.IsEnabled = false;
            searchButton.IsEnabled = false;
        }

        private void DeparturePlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
            if (DepartureStation != null && ArrivalStation != null)
            {
                searchButton.IsEnabled = true;
            }
        }

        private void ArrivalPlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
            if (DepartureStation != null && ArrivalStation != null)
            {
                searchButton.IsEnabled = true;
            }
        }

        private void Datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartureDate != today)
            {
                clearButton.IsEnabled = true;
            }
        }
    }
}
