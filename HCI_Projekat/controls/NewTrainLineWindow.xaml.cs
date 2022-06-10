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
    /// Interaction logic for NewTrainLineWindow.xaml
    /// </summary>
    public partial class NewTrainLineWindow : Window
    {
        public string Code { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public ObservableCollection<string> Stations { get; set; } = new ObservableCollection<string>();
        StationInputDialog stationInputDialog;

        public delegate void OnClose();
        public event OnClose OnCloseHandler;

        public delegate void OnTrainLineAdded(TrainLine trainLine);
        public event OnTrainLineAdded TrainLineAdded;

        public NewTrainLineWindow()
        {
            InitializeComponent();
            DataContext = this;
            DeleteButtonStation.IsEnabled = false;
        }

        private void AddButtonStation_Click(object sender, RoutedEventArgs e)
        {
            AddButtonStation.IsEnabled = false;
            stationInputDialog = new StationInputDialog();
            stationInputDialog.StationAdded += AddStation;
            stationInputDialog.WindowClosed += SwitchStationAddButtonEnable;
            stationInputDialog.Show();
        }

        private void DeleteButtonStation_Click(object sender, RoutedEventArgs e)
        {
            Stations.Remove((string)stations.SelectedItem);
            DeleteButtonStation.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TrainLine trainLine = new TrainLine(Code, DeparturePlace, ArrivalPlace, Stations.ToArray());
            TrainLineAdded?.Invoke(trainLine);
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteButtonStation.IsEnabled = true;
        }

        private void AddStation(string name)
        {
            if (Stations.Contains(name))
            {
                MessageBox.Show("Станица већ постоји. Није могуће додати две исте станице", "Станица постоји", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Stations.Add(name);
                stations.UpdateLayout();
            }
        }

        private void SwitchStationAddButtonEnable()
        {
            AddButtonStation.IsEnabled = true;
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
                    MoveStation((string)(e.Data.GetData(DataFormats.Serializable)), (string)(element.DataContext));
                }
            }
            catch (Exception) { }
        }

        public void MoveStation(string station1, string station2)
        {
            Stations.Remove(station1);
            int index = Stations.IndexOf(station2);
            Stations.Insert(index, station1);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OnCloseHandler?.Invoke();
        }
    }
}
