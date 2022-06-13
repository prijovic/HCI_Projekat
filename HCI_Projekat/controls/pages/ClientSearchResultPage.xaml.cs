using HCI_Projekat.model;
using HCI_Projekat.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for ClientSearchResultPage.xaml
    /// </summary>
    public partial class ClientSearchResultPage : Page
    {
        public ObservableCollection<SearchResultItem> ScheduleItems { get; set; }
        private string Username { get; set; }
        private TicketService ticketService;

        public ClientSearchResultPage(ref TicketService ticketService, SortedSet<ScheduleItem> scheduleItems, SearchLine searchLine, string username)
        {
            List<SearchResultItem> searchResults = new List<SearchResultItem>();
            Username = username;
            this.ticketService = ticketService;
            foreach (ScheduleItem si in scheduleItems)
            {
                searchResults.Add(new SearchResultItem(si, searchLine));
            }
            ScheduleItems = new ObservableCollection<SearchResultItem>(searchResults);
            InitializeComponent();
            DataContext = this;
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchResultItem searchResult = (SearchResultItem)((Button)e.Source).DataContext;
                Ticket ticket = new Ticket(UserService.GetUserByUsernameS(Username), DateTime.Now, searchResult, false);
                ticketService.AddTicket(ticket);
                MessageBox.Show("Успешно сте резервисали карту.", "Успешна резервација", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchResultItem searchResult = (SearchResultItem)((Button)e.Source).DataContext;
                Ticket ticket = new Ticket(UserService.GetUserByUsernameS(Username), DateTime.Now, searchResult, true);
                ticketService.AddTicket(ticket);
                MessageBox.Show("Успешно сте купили карту.", "Успешна куповина", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
