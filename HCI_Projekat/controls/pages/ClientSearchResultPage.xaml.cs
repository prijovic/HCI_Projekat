using HCI_Projekat.model;
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

        public ClientSearchResultPage(SortedSet<ScheduleItem> scheduleItems, SearchLine searchLine)
        {
            List<SearchResultItem> searchResults = new List<SearchResultItem>();
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
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ProductName = dataRowView[1].ToString();
                String ProductDescription = dataRowView[2].ToString();
                MessageBox.Show("You Clicked : " + ProductName + "\r\nDescription : " + ProductDescription);
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
            }
        }
    }
}
