using HCI_Projekat.controls.pages;
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

namespace HCI_Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserService userService = new UserService();
        TrainLineService trainLineService = new TrainLineService();
        TrainService trainService = new TrainService();
        TicketService ticketService = new TicketService();
        ScheduleItemService scheduleItemService = new ScheduleItemService();

        public MainWindow()
        {
            InitializeComponent();
            mainPage.Content = new LoginPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainPage.Content = new RegistrationPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

    }
}
