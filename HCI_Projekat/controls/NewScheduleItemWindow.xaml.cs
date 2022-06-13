using HCI_Projekat.model;
using HCI_Projekat.services;
using HCI_Projekat.touring;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using ThinkSharp.FeatureTouring.Models;
using ThinkSharp.FeatureTouring.Navigation;

namespace HCI_Projekat.controls
{
    /// <summary>
    /// Interaction logic for NewScheduleItemWindow.xaml
    /// </summary>
    public partial class NewScheduleItemWindow : Window, INotifyPropertyChanged
    {
        public delegate void OnClose();
        public event OnClose OnCloseHandler;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public NewScheduleItemWindow()
        {
            InitializeComponent();
            AddLines();
            AddTrains();
            StationArrivals = new List<StationArrival>();
        }

        public TrainLine TrainLine
        {
            get;
            set;
        }

        public Train Train
        {
            get;
            set;
        }

        public List<StationArrival> StationArrivals
        {
            get;
            set;
        }

        public DateTime StationTime
        {
            get;
            set;
        }

        public double Price
        {
            get; set;
        }

        private void AddLines()
        {
            TrainLineService.TrainLines.ForEach(tl => { line.Items.Add(tl); });
        }

        private void AddTrains()
        {
            TrainService.Trains.ForEach(t => { train.Items.Add(t); });
        }

        private void AddStations()
        {
            TrainLine?.Stations.ForEach(s => {
                station.Items.Add(s);
                station.SelectedItem = s;
                StationArrivals.Add(new StationArrival(s));
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OnCloseHandler?.Invoke();
        }

        private void Line_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationArrivals = new List<StationArrival>();
            TrainLine = (TrainLine)line.SelectedItem;
            AddStations();
        }

        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationArrival stationArrival = GetStationArrival((Station)station.SelectedItem);
            if (stationArrival != null)
            {
                Price = stationArrival.Price;
                StationTime = stationArrival.Time;
            }
        }

        private StationArrival GetStationArrival(Station station)
        {
            foreach (StationArrival sa in StationArrivals)
            {
                if (sa.Station == station)
                {
                    return sa;
                }
            }
            return null;
        }

        private void Datetimepicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        public static void StartAddScheduleItemTour()
        {
            var tour = new Tour
            {
                Name = "New schedule item tour",
                ShowNextButtonDefault = true,
                Steps = new[]
                {
                    new Step(ElementID.DepartureDateTime, "Полазак", "Одаберите датум и време поласка."),
                    new Step(ElementID.ArrivalDateTime, "Долазак", "Одаберите датум и време доласка."),
                    new Step(ElementID.TextBoxTotalPrice, "Цена целе вожње", "Унесите цену карте целе вожње, нпр. 800."),
                    new Step(ElementID.ComboBoxLine, "Линија", "Одаберите једну од понуђених линија."),
                    new Step(ElementID.ComboBoxTrain, "Воз", "Одаберите један од понуђених возова."),
                    new Step(ElementID.ComboBoxStation, "Станица", "Одаберите једну од понуђених станица на датој линији."),
                    new Step(ElementID.ArrivalStationTime, "Долазак на станицу", "Одаберите датум и време доласка на станицу."),
                    new Step(ElementID.TextBoxPartialPrice, "Цена до станице", "Унесите цену карте до станице, нпр. 400."),
                    new Step(ElementID.BtnAddScheduleItem, "Додај ставку", "Притисните дугме уколико желите \n да додате нову ставку реда вожње."),
                }
            };
            tour.Start();
        }

        private void Btn_Schedule_Item_Tutorial_Click(object sender, RoutedEventArgs e)
        {
            FeatureTour.SetViewModelFactoryMethod(tourRun => new CustomTourViewModel(tourRun));
       
            var navigator = FeatureTour.GetNavigator();

            navigator.OnStepEntered(ElementID.DepartureDateTime).Execute(s => DepartureTime.Focus());
            navigator.OnStepEntered(ElementID.ArrivalDateTime).Execute(s => ArrivalTime.Focus());
            navigator.OnStepEntered(ElementID.TextBoxTotalPrice).Execute(s => TotalPrice.Focus());
            navigator.OnStepEntered(ElementID.ComboBoxLine).Execute(s => line.Focus());
            navigator.OnStepEntered(ElementID.ComboBoxTrain).Execute(s => train.Focus());
            navigator.OnStepEntered(ElementID.ComboBoxStation).Execute(s => station.Focus());
            navigator.OnStepEntered(ElementID.ArrivalStationTime).Execute(s => datetimepicker.Focus());
            navigator.OnStepEntered(ElementID.TextBoxPartialPrice).Execute(s => PartialPrice.Focus());
            navigator.OnStepEntered(ElementID.BtnAddScheduleItem).Execute(s => AddButton.Focus());

            DepartureTime.ValueChanged += departureTimeSelected;
            ArrivalTime.ValueChanged += arrivalTimeSelected;
            TotalPrice.TextChanged += totalPriceTextChanged;
            line.SelectionChanged += lineSelected;
            train.SelectionChanged += trainSelected;
            station.SelectionChanged += stationSelected;
            datetimepicker.ValueChanged += stationTimeSelected;
            PartialPrice.TextChanged += partialPriceTextChanged;

            AddButton.Click += btnAddScheduleItemClicked;

            StartAddScheduleItemTour();
        }

        private void btnAddScheduleItemClicked(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.BtnAddScheduleItem).Close();
        }

        private void departureTimeSelected(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.DepartureDateTime).GoNext();
        }

        private void arrivalTimeSelected(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.ArrivalDateTime).GoNext();
        }

        private void totalPriceTextChanged(object sender, RoutedEventArgs e)
        {
            if(Price != 0)
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxTotalPrice).GoNext();
            }
        }

        private void lineSelected(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.ComboBoxLine).GoNext();
        }

        private void trainSelected(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.ComboBoxTrain).GoNext();
        }
        private void stationSelected(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.ComboBoxStation).GoNext();
        }

        private void stationTimeSelected(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.ArrivalStationTime).GoNext();
        }

        private void partialPriceTextChanged(object sender, RoutedEventArgs e)
        {
            if(Price != 0)
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxPartialPrice).GoNext();
            }
        }
    }
}
