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
    /// Interaction logic for ManagerTrainLines.xaml
    /// </summary>
    public partial class ManagerTrainLines : Page, INotifyPropertyChanged
    {
        TrainLineService trainLineService;
        TrainService trainService;
        StationInputDialog stationInputDialog;
        NewTrainLineWindow window;

        public ManagerTrainLines(ref TrainLineService trainLineService, ref TrainService trainService)
        {
            this.trainService = trainService;
            this.trainLineService = trainLineService;
            TrainLines = new ObservableCollection<TrainLine>(TrainLineService.TrainLines);
            InitializeComponent();
            DataContext = this;
            DeleteButton.IsEnabled = false;
            DeleteButtonStation.IsEnabled = false;
            AddButtonStation.IsEnabled = false;
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

        public ObservableCollection<TrainLine> TrainLines
        {
            get;
            set;
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DeleteButton.IsEnabled = true;
            stationInputDialog?.Close();
            AddButtonStation.IsEnabled = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TrainLine trainLine = (TrainLine)trainsManager.SelectedItem;
            trainLineService.RemoveTrainLine(trainLine);
            TrainLines.Remove(trainLine);
            DeleteButton.IsEnabled = false;
        }

        private void ListBoxItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is FrameworkElement frameworkElement)
            {
                object station = frameworkElement.DataContext;

                DragDropEffects dragDropResult = DragDrop.DoDragDrop(frameworkElement,
                    new DataObject(DataFormats.Serializable, station),
                    DragDropEffects.Move);
            }
        }

        private void Stations_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (sender is FrameworkElement element)
                {
                    ((TrainLine)trainsManager.SelectedItem).MoveStation((Station)(e.Data.GetData(DataFormats.Serializable)), (Station)(element.DataContext));
                }
            }
            catch (Exception) { }
        }

        private void DeleteButtonStation_Click(object sender, RoutedEventArgs e)
        {
            ((TrainLine)trainsManager.SelectedItem).RemoveStation((Station)stations.SelectedItem);
            DeleteButtonStation.IsEnabled = false;
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Station)stations.SelectedItem != null)
            {
                DeleteButtonStation.IsEnabled = true;
            } else
            {
                DeleteButtonStation.IsEnabled = false;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            window = new NewTrainLineWindow();
            window.TrainLineAdded += AddTrainLine;
            window.OnCloseHandler += ActivateAddButton;
            window.Show();
            AddButton.IsEnabled = false;
        }

        private void AddButtonStation_Click(object sender, RoutedEventArgs e)
        {
            AddButtonStation.IsEnabled = false;
            stationInputDialog = new StationInputDialog();
            stationInputDialog.StationAdded += AddStation;
            stationInputDialog.WindowClosed += SwitchStationAddButtonEnable;
            stationInputDialog.Show();
        }

        private void AddStation(string stationName)
        {
            ((TrainLine)trainsManager.SelectedItem).AddStataion(stationName);
        }

        private void SwitchStationAddButtonEnable()
        {
            AddButtonStation.IsEnabled = true;
        }

        private void AddTrainLine(TrainLine trainLine)
        {
            TrainLines.Add(trainLine);
            trainLineService.AddTrainLine(trainLine);
        }

        private void ActivateAddButton()
        {
            AddButton.IsEnabled = true;
        }
    }
}
