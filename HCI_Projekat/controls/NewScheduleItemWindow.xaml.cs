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
        public ObservableCollection<double> Prices { get; set; }
        public ObservableCollection<DateTime> StationsTime { get; set; }

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
            DataContext = this;
            AddLines();
            AddTrains();
        }

        public TrainLine TrainLine
        {
            get;
            set;
        }

        public DateTime DepartureTime
        {
            get;
            set;
        }

        public DateTime ArrivalTime
        {
            get;
            set;
        }

        public Train Train
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

        public double StationPrice
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

        private void Window_Closed(object sender, EventArgs e)
        {
            OnCloseHandler?.Invoke();
        }

        private void Line_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrainLine = (TrainLine)line.SelectedItem;
            station.Items.Clear();
            StationsTime = new ObservableCollection<DateTime>(new List<DateTime>(TrainLine.Stations.Count));
            Prices = new ObservableCollection<double>(new List<double>(TrainLine.Stations.Count));
            TrainLine.Stations.ForEach(s => {
                station.Items.Add(s);
                StationsTime.Add(default);
                Prices.Add(default);
            });
        }

        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station s = (Station)station.SelectedItem;
            StationTime = StationsTime.ElementAt(TrainLine.Stations.IndexOf(s));
            StationPrice = Prices.ElementAt(TrainLine.Stations.IndexOf(s));
        }

        private void Datetimepicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            StationTime = (DateTime)datetimepicker.Value;
            Station s = (Station)station.SelectedItem;
            if (s != null)
            {
                StationsTime.Insert(TrainLine.Stations.IndexOf(s), StationTime);
                StationsTime.RemoveAt(TrainLine.Stations.IndexOf(s) + 1);
            }

        }

        private void DepartureDatetimepicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DepartureTime = (DateTime)DepartureTimePicker.Value;
        }

        private void ArrivalDatetimepicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ArrivalTime = (DateTime)ArrivalTimePicker.Value;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleItem scheduleItem = new ScheduleItem(TrainLine, DepartureTime, ArrivalTime, StationsTime.ToArray(), Train, Price, Prices.ToArray());
            OnScheduleItemAdded?.Invoke(scheduleItem);
        }
    }
}
