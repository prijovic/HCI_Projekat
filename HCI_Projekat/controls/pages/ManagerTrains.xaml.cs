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
    /// Interaction logic for ManagerTrains.xaml
    /// </summary>
    public partial class ManagerTrains : Page, INotifyPropertyChanged
    {
        TrainService trainService;

        public ManagerTrains(ref TrainService trainService)
        {
            this.trainService = trainService;
            Trains = new ObservableCollection<Train>(TrainService.Trains);
            InitializeComponent();
            DeleteButton.IsEnabled = false;
            DataContext = this;
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

        public ObservableCollection<Train> Trains
        {
            get;
            set;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Train train = (Train)trainsManager.SelectedItem;
            trainService.RemoveTrain(train);
            Trains.Remove(train);
            DeleteButton.IsEnabled = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NewTrainWindow window = new NewTrainWindow(ref trainService);
            window.TrainAdded += AddTrain;
            window.OnCloseHandler += ActivateAddButton;
            window.Show();
            AddButton.IsEnabled = false;
        }

        public void AddTrain(Train train)
        {
            Trains.Add(train);
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DeleteButton.IsEnabled = true;
        }

        private void ActivateAddButton()
        {
            AddButton.IsEnabled = true;
        }
    }
}
