using HCI_Projekat.services;
using LiveCharts;
using LiveCharts.Wpf;
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
    /// Interaction logic for ManagerHomePage.xaml
    /// </summary>
    public partial class ManagerHomePage : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public double TotalTickets { get; set; } = TicketService.Tickets.Count;
        public double TicketsInMonth { get; set; } = TicketService.CountTicketsInMonth(DateTime.Now.Month, DateTime.Now.Year);
        public double TicketsInYear { get; set; }

        public ManagerHomePage()
        {
            Dictionary<string, double> graph = TicketService.GetTicketGraphData();
            TicketsInYear = graph.Values.Sum();
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Годишњи преглед продатих карата по месецима",
                    Values = new ChartValues<double>(graph.Values.Reverse()),
                    LineSmoothness = 1,
                    DataLabels=true,
                    FontSize=15,
                }
            };

            Labels = graph.Keys.Reverse().ToArray();
            YFormatter = value => value.ToString();

            DataContext = this;

            valueTitle.Title = "Број продатих карата";
        }
    }
}
