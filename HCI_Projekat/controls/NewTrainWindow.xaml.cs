using HCI_Projekat.model;
using HCI_Projekat.services;
using HCI_Projekat.touring;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NewTrainWindow.xaml
    /// </summary>
    public partial class NewTrainWindow : Window
    {
        TrainService trainService;
        public string TrainName { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }

        public delegate void TrainAddedHandler(Train train);
        public event TrainAddedHandler TrainAdded;

        public delegate void OnClose();
        public event OnClose OnCloseHandler;

        protected void OnTrainAdded(Train train)
        {
            TrainAdded?.Invoke(train);
        }

        public NewTrainWindow(ref TrainService trainService)
        {
            this.trainService = trainService;
            InitializeComponent();
            DataContext = this;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Train newTrain = new Train(TrainName, Code, Capacity);
            trainService.AddTrain(newTrain);
            OnTrainAdded(newTrain);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OnCloseHandler?.Invoke();
        }

        public static void StartAddTrainTour()
        {
            var tour = new Tour
            {
                Name = "New train tour",
                ShowNextButtonDefault = true,
                Steps = new[]
                {
                    new Step(ElementID.TextBoxTrainCode, "Унесите шифру воза", "Нпр. AF345"),
                    new Step(ElementID.TextBoxTrainName, "Унесите назив воза", "Нпр. \"Regio1\""),
                    new Step(ElementID.TextBoxTrainCapacity, "Унесите капацитет воза", "Нпр. 200"),
                    new Step(ElementID.BtnAddNewTrain, "Додај воз", "Притисните дугме да додате воз..")
                }
            };
            tour.Start();
        }

        private void Btn_Train_Tutorial_Click(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();

            navigator.OnStepEntered(ElementID.TextBoxTrainCode).Execute(s => TextTrainCode.Focus());
            navigator.OnStepEntered(ElementID.TextBoxTrainName).Execute(s => TextTrainName.Focus());
            navigator.OnStepEntered(ElementID.TextBoxTrainCapacity).Execute(s => TextTrainCapacity.Focus());
            navigator.OnStepEntered(ElementID.BtnAddNewTrain).Execute(s => ButtonAddTrain.Focus());

            TextTrainCode.TextChanged += trainCodeTextChanged;
            TextTrainName.TextChanged += trainNameTextChanged;
            TextTrainCapacity.TextChanged += trainCapacityTextChanged;
            ButtonAddTrain.Click += addClicked;

            StartAddTrainTour();
        }

        private void addClicked(object sender, RoutedEventArgs e)
        {
            var navigator = FeatureTour.GetNavigator();
            navigator.IfCurrentStepEquals(ElementID.BtnAddNewTrain).Close();
        }

        private void trainCodeTextChanged(object sender, RoutedEventArgs e)
        {
            if (Code != null && Code != "")
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxTrainCode).GoNext();
            }
        }

        private void trainNameTextChanged(object sender, RoutedEventArgs e)
        {
            if (TrainName != null && TrainName != "")
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxTrainName).GoNext();
            }
        }

        private void trainCapacityTextChanged(object sender, RoutedEventArgs e)
        {
            if (Capacity != 0)
            {
                var navigator = FeatureTour.GetNavigator();
                navigator.IfCurrentStepEquals(ElementID.TextBoxTrainCapacity).GoNext();
            }
        }

    }
}
