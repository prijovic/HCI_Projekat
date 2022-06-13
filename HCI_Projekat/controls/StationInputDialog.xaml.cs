using HCI_Projekat.model;
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
using System.Windows.Shapes;

namespace HCI_Projekat.controls
{
    /// <summary>
    /// Interaction logic for StationInputDialog.xaml
    /// </summary>
    public partial class StationInputDialog : Window
    {
        public delegate void OnStationAdded(string stationName);
        public delegate void OnWindowClose();
        public event OnStationAdded StationAdded;
        public event OnWindowClose WindowClosed;
        public string StationName { get; set; }

        public StationInputDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StationName != null)
            {
                StationAdded?.Invoke(StationName);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WindowClosed?.Invoke();
        }
    }
}
