using HCI_Projekat.model;
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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        UserService userService;
        TrainLineService trainLineService;
        TicketService ticketService;
        ScheduleItemService scheduleItemService;
        TrainService trainService;

        public RegistrationPage(ref UserService userService, ref TrainLineService trainLineService, ref TicketService ticketService, ref ScheduleItemService scheduleItemService, ref TrainService trainService)
        {
            this.userService = userService;
            this.trainLineService = trainLineService;
            this.ticketService = ticketService;
            this.scheduleItemService = scheduleItemService;
            this.trainService = trainService;
            InitializeComponent();
            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).mainPage.Content = new LoginPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(UserName, Surname, Username, password.Password, UserType.Client);
            if (userService.AddUser(user))
            {
                ((MainWindow)App.Current.MainWindow).mainPage.Content = new LoginPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
                MessageBox.Show($"Успешно сте се регистровали корисничким именом: {Username}.", "Успешна регистрација!");
            }
            else
            {
                MessageBox.Show("NEUSPEH");
            }
        }
    }
}
