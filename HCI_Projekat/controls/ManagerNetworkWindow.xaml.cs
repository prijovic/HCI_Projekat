using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for ManagerNetworkWindow.xaml
    /// </summary>
    public partial class ManagerNetworkWindow : Window
    {
        public delegate void OnClose();
        public event OnClose ClosedWindow;

        private static Uri uri;

        public ManagerNetworkWindow()
        {
            uri = App.NETWORK_URI;
            InitializeComponent();
            network.Source = new BitmapImage(uri);
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    Console.WriteLine(files[0]);
                    Uri uri = new Uri(files[0]);
                    var image = new BitmapImage(uri);
                    MessageBoxResult result = MessageBox.Show("Да ли желите да промените мрежни приказ линија?", "Промена мреже линија", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            Bitmap file;
                            using (MemoryStream outStream = new MemoryStream())
                            {
                                BitmapEncoder enc = new BmpBitmapEncoder();
                                enc.Frames.Add(BitmapFrame.Create(image));
                                enc.Save(outStream);
                                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                                file = new Bitmap(bitmap);
                            }
                            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\")) + "/assets/image.jpg";
                            App.NETWORK_URI = new Uri(path);
                            file.Save(path);
                            network.Source = new BitmapImage(uri);
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Погрешан тип фајла. Покушајте да додате слику.", "Погрешан тип фајла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ClosedWindow?.Invoke();
        }
    }
}
