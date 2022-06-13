using HCI_Projekat.model;
using HCI_Projekat.services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat.controls.pages
{
    /// <summary>
    /// Interaction logic for ClientTicketsPage.xaml
    /// </summary>
    public partial class ClientTicketsPage : Page, INotifyPropertyChanged
    {

        public ClientTicketsPage(ref TicketService ticketService, string username)
        {
            Tickets = new ObservableCollection<Ticket>(ticketService.GetUsersTickets(username));
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private ICollectionView _View;
        public ICollectionView View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }

        public ObservableCollection<Ticket> Tickets
        {
            get;
            set;
        }
    }
}
