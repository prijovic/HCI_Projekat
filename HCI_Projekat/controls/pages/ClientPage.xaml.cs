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
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        UserService userService;
        TrainLineService trainLineService;
        TicketService ticketService;
        ScheduleItemService scheduleItemService;
        TrainService trainService;
        string Username { get; set; }
        private bool NetworkOpened { get; set; } = false;

        public ClientPage(ref UserService userService, ref TrainLineService trainLineService, ref TicketService ticketService, ref ScheduleItemService scheduleItemService, ref TrainService trainService, string username)
        {
            this.userService = userService;
            this.trainLineService = trainLineService;
            this.ticketService = ticketService;
            this.scheduleItemService = scheduleItemService;
            this.trainService = trainService;
            Username = username;
            InitializeComponent();
            clientPage.Content = new ClientLinesPage(ref ticketService, ref trainLineService, ref scheduleItemService, Username);
        }

        private void Tickets_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Tickets_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clientPage.Content = new ClientTicketsPage(ref ticketService, Username);
        }

        private void Logout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (Username != null);
        }

        private void Logout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).mainPage.Content = new LoginPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

        private void Network_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !NetworkOpened;
        }

        private void Network_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClientNetworkWindow managerNetworkWindow = new ClientNetworkWindow();
            managerNetworkWindow.ClosedWindow += CloseNetwork;
            managerNetworkWindow.Show();
            NetworkOpened = true;
        }

        private void Home_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Home_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clientPage.Content = new ClientLinesPage(ref ticketService,ref trainLineService, ref scheduleItemService, Username);
        }

        private void CloseNetwork()
        {
            NetworkOpened = false;
        }
    }
}
