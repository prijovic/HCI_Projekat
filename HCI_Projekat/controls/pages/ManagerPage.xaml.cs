using HCI_Projekat.services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManagerPage.xaml
    /// </summary>      
    public partial class ManagerPage : Page
    {
        UserService userService;
        TrainLineService trainLineService;
        TicketService ticketService;
        ScheduleItemService scheduleItemService;
        TrainService trainService;
        private bool NetworkOpened { get; set; } = false;
        string Username { get; set; }

        public ManagerPage(ref UserService userService, ref TrainLineService trainLineService, ref TicketService ticketService, ref ScheduleItemService scheduleItemService, ref TrainService trainService, string username)
        {
            this.userService = userService;
            this.trainLineService = trainLineService;
            this.ticketService = ticketService;
            this.scheduleItemService = scheduleItemService;
            this.trainService = trainService;
            Username = username;
            InitializeComponent();
            managerPage.Content = new ManagerHomePage();
        }

        private void Schedule_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Schedule_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            managerPage.Content = new ManagerSchedule(ref scheduleItemService);
        }

        private void TrainLines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TrainLines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            managerPage.Content = new ManagerTrainLines(ref trainLineService, ref trainService);
        }

        private void Trains_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Trains_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            managerPage.Content = new ManagerTrains(ref trainService);
        }

        private void Network_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !NetworkOpened;
        }

        private void Network_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ManagerNetworkWindow managerNetworkWindow = new ManagerNetworkWindow();
            managerNetworkWindow.ClosedWindow += CloseNetwork;
            managerNetworkWindow.Show();
            NetworkOpened = true;
        }

        private void Logout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Username != null);
        }

        private void Logout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).mainPage.Content = new LoginPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

        public void CloseNetwork()
        {
            NetworkOpened = false;
        }

        private void Home_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Home_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            managerPage.Content = new ManagerHomePage();
        }

        private void TicketsMonth_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TicketsMonth_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Console.Beep();
        }

        private void TicketsRide_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TicketsRide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Console.WriteLine("Karteeee");
        }
    }
}
