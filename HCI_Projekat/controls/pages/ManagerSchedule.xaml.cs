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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat.controls.pages
{
    /// <summary>
    /// Interaction logic for ManagerSchedule.xaml
    /// </summary>
    public partial class ManagerSchedule : Page, INotifyPropertyChanged
    {
        ScheduleItemService itemService;
        NewScheduleItemWindow window;

        public ManagerSchedule(ref ScheduleItemService scheduleItemService)
        {
            itemService = scheduleItemService;
            ScheduleItems = new ObservableCollection<ScheduleItem>(ScheduleItemService.ScheduleItems);
            InitializeComponent();
            DataContext = this;
            AddTrainLines();
            AddTrains();
            DeleteButton.IsEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private ICollectionView _View;
        public ICollectionView View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }

        public ObservableCollection<ScheduleItem> ScheduleItems
        {
            get;
            set;
        }

        private void AddTrains()
        {
            TrainService.Trains.ForEach((t) =>
            {
                train.Items.Add(t);
            });
        }

        private void AddTrainLines()
        {
            TrainLineService.TrainLines.ForEach((tl) =>
            {
                line.Items.Add(tl);
            });
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScheduleItem)scheduleManager.SelectedItem)?.StationArrivals.ForEach(s => {
                station.Items.Add(s.Station);
                station.SelectedItem = s.Station;
            });
        }

        private double _stationPrice;
        public double StationPrice
        {
            get
            {
                return _stationPrice;
            }
            set
            {
                if (value != _stationPrice)
                {
                    itemService.SetPrice((Station)station.SelectedItem, value);
                    ScheduleItems = new ObservableCollection<ScheduleItem>(ScheduleItemService.ScheduleItems);
                    _stationPrice = value;
                    OnPropertyChanged("StationPrice");
                }
            }
        }

        private DateTime _stationArrival;
        public DateTime StationArrival
        {
            get
            {
                return _stationArrival;
            }
            set
            {
                if (value == default(DateTime))
                {
                    DateTime time = itemService.GetTime((Station)station.SelectedItem);
                    if (time != default(DateTime))
                    {
                        _stationArrival = itemService.GetTime((Station)station.SelectedItem);
                    }
                }
                else if (value != _stationArrival)
                {
                    itemService.SetTime((Station)station.SelectedItem, value);
                    ScheduleItems = new ObservableCollection<ScheduleItem>(ScheduleItemService.ScheduleItems);
                    _stationArrival = value;
                    OnPropertyChanged("StationArrival");
                }
            }
        }

        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationArrival = itemService.GetTime((Station)station.SelectedItem);
            StationPrice = itemService.GetPrice((Station)station.SelectedItem);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ScheduleItem scheduleItem = (ScheduleItem)scheduleManager.SelectedItem;
            itemService.RemoveScheduleItem(scheduleItem);
            ScheduleItems = new ObservableCollection<ScheduleItem>(ScheduleItemService.ScheduleItems);
            OnPropertyChanged("ScheduleItems");
            DeleteButton.IsEnabled = false;
        }

        private void ScheduleManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteButton.IsEnabled = true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            window = new NewScheduleItemWindow();
            window.OnCloseHandler += ActivateAddButton;
            window.OnScheduleItemAdded += AddScheduleItem;
            window.Show();
            AddButton.IsEnabled = false;
        }

        private void ActivateAddButton()
        {
            AddButton.IsEnabled = true;
        }

        private void AddScheduleItem(ScheduleItem scheduleItem)
        {
            ScheduleItems.Add(scheduleItem);
            itemService.AddScheduleItem(scheduleItem);
        }
    }
}
