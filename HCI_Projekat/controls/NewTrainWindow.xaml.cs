using HCI_Projekat.model;
using HCI_Projekat.services;
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
    }
}
