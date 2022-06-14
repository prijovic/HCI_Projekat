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

        public static void StartAddTrainLineTour()
        {
            var tour = new Tour
            {
                Name = "New train line tour",
                ShowNextButtonDefault = true,
                Steps = new[]
                {
                    new Step(ElementID.TextBoxCode, "Унесите шифру линије", "Нпр. \"BGNSS\""),
                    new Step(ElementID.TextBoxDeparturePlace, "Унесите полазиште", "Нпр. Београд"),
                    new Step(ElementID.TextBoxArrivalPlace, "Унесите одредиште", "Нпр. Нови Сад"),
                    new Step(ElementID.BtnAddNewStation, "Додај станицу", "Притисните дугме уколико линија није директна него желите да додате станицу."),
                    new Step(ElementID.TextBoxStation, "Унесите назив станице", "Нпр. Нови Београд"),
                    new Step(ElementID.BtnStation, "Додај", "Притисните дугме да додате станицу."),
                    new Step(ElementID.BtnAddTrainLine, "Додај", "Притисните дугме да додате нову линију.")

                }
            };
            tour.Start();
        }

        private void Tutorial_Executed(object sender, RoutedEventArgs e)
        {
            FeatureTour.SetViewModelFactoryMethod(tourRun => new CustomTourViewModel(tourRun));
            stationInputDialog = new StationInputDialog();
            var navigator = FeatureTour.GetNavigator();

            navigator.OnStepEntered(ElementID.TextBoxCode).Execute(s => CodeTrain.Focus());
            navigator.OnStepEntered(ElementID.TextBoxDeparturePlace).Execute(s => TrainDeparturePlace.Focus());
            navigator.OnStepEntered(ElementID.TextBoxArrivalPlace).Execute(s => TrainArrivalPlace.Focus());
            navigator.OnStepEntered(ElementID.BtnAddNewStation).Execute(s => AddButtonStation.Focus());
            navigator.OnStepEntered(ElementID.TextBoxStation).Execute(s => stationInputDialog.StationNameBox.Focus());
            navigator.OnStepEntered(ElementID.BtnStation).Execute(s => stationInputDialog.ButtonAddStation.Focus());
            navigator.OnStepEntered(ElementID.BtnAddTrainLine).Execute(s => AddButton.Focus());

            CodeTrain.TextChanged += codeTextChanged;
            TrainDeparturePlace.TextChanged += departurePlaceTextChanged;
            TrainArrivalPlace.TextChanged += arrivalPlaceTextChanged;
            AddButtonStation.Click += addNewStationClicked;
            stationInputDialog.StationNameBox.TextChanged += stationNameTextChanged;
            stationInputDialog.ButtonAddStation.Click += addStationClicked;

            AddButton.Click += addClicked;

            StartAddTrainLineTour();
        }

        private void Tutorial_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void addClicked(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.BtnAddTrainLine).Close();
        }

        private void codeTextChanged(object sender, RoutedEventArgs e)
        {
            if (Code != null && Code != "")
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxCode).GoNext();
            }
        }

        private void departurePlaceTextChanged(object sender, RoutedEventArgs e)
        {
            if (DeparturePlace != null && DeparturePlace != "")
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxDeparturePlace).GoNext();
            }
        }

        private void arrivalPlaceTextChanged(object sender, RoutedEventArgs e)
        {
            if (ArrivalPlace != null && ArrivalPlace != "")
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxArrivalPlace).GoNext();
            }
        }

        private void addNewStationClicked(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.BtnAddNewStation).GoNext();
        }

        private void stationNameTextChanged(object sender, RoutedEventArgs e)
        {
            if (stationInputDialog.StationName != null && stationInputDialog.StationName != "")
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxStation).GoNext();
            }
        }

        private void addStationClicked(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.BtnStation).GoNext();
        }
    }
}
