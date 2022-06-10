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

        public ClientPage(ref UserService userService, ref TrainLineService trainLineService, ref TicketService ticketService, ref ScheduleItemService scheduleItemService, ref TrainService trainService, string username)
        {
            this.userService = userService;
            this.trainLineService = trainLineService;
            this.ticketService = ticketService;
            this.scheduleItemService = scheduleItemService;
            this.trainService = trainService;
            Username = username;
            InitializeComponent();
            clientPage.Content = new ClientLinesPage(ref trainLineService);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).mainPage.Content = new LoginPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

        private void Tickets_Click(object sender, RoutedEventArgs e)
        {
            clientPage.Content = new ClientTicketsPage(ref ticketService, Username);
        }

        private void Lines_Click(object sender, RoutedEventArgs e)
        {
            clientPage.Content = new ClientLinesPage(ref trainLineService);
        }
    }
}
