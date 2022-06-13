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
    /// Interaction logic for ClientNetworkWindow.xaml
    /// </summary>
    public partial class ClientNetworkWindow : Window
    {
        public delegate void OnClose();
        public event OnClose ClosedWindow;
        private static Uri uri;

        public ClientNetworkWindow()
        {
            uri = App.NETWORK_URI;
            InitializeComponent();
            network.Source = new BitmapImage(uri);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosedWindow?.Invoke();
        }
    }
}
