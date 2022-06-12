using HCI_Projekat.model;
using HCI_Projekat.services;
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

namespace HCI_Projekat.controls
{
    /// <summary>
    /// Interaction logic for NewScheduleItemWindow.xaml
    /// </summary>
    public partial class NewScheduleItemWindow : Window, INotifyPropertyChanged
    {
        public delegate void OnClose();
        public event OnClose OnCloseHandler;

        public delegate void OnScheduleItemAddedHandler(ScheduleItem scheduleItem);
        public event OnScheduleItemAddedHandler OnScheduleItemAdded;

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
    }
}
