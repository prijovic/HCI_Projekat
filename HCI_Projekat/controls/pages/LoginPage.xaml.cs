using HCI_Projekat.model;
using HCI_Projekat.services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, INotifyPropertyChanged
    {
        UserService userService;
        TrainLineService trainLineService;
        TicketService ticketService;
        ScheduleItemService scheduleItemService;
        TrainService trainService;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public LoginPage(ref UserService userService, ref TrainLineService trainLineService, ref TicketService ticketService, ref ScheduleItemService scheduleItemService, ref TrainService trainService)
        {
            Username = "";
            this.userService = userService;
            this.trainLineService = trainLineService;
            this.ticketService = ticketService;
            this.scheduleItemService = scheduleItemService;
            this.trainService = trainService;
            InitializeComponent();
            DataContext = this;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)App.Current.MainWindow).mainPage.Content = new RegistrationPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = Username;
            string message = userService.LoginUser(username, this.passwordBox.Password);
            if (message == null)
            {
                string type = userService.GetUserType(username);
                if (type == UserType.Client.ToString())
                {
                    ((MainWindow)App.Current.MainWindow).mainPage.Content = new ClientPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService, username);
                }
                else if (type == UserType.Manager.ToString())
                {
                    ((MainWindow)App.Current.MainWindow).mainPage.Content = new ManagerPage(ref userService, ref trainLineService, ref ticketService, ref scheduleItemService, ref trainService, username);
                }
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
