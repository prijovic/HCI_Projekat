using HCI_Projekat.controls.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCI_Projekat.controls
{
    public static class ClientCommands
    {
        public static readonly RoutedUICommand Tickets = new RoutedUICommand("Карте", "Карте", typeof(ClientPage), new InputGestureCollection() { new KeyGesture(Key.K, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Logout = new RoutedUICommand("Одјави се", "Одјави се", typeof(ClientPage), new InputGestureCollection() { new KeyGesture(Key.Escape, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Network = new RoutedUICommand("Мрежа линија", "Мрежа линија", typeof(ClientPage), new InputGestureCollection() { new KeyGesture(Key.M, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Home = new RoutedUICommand("Почетна страница", "Почетна страница", typeof(ClientPage), new InputGestureCollection() { new KeyGesture(Key.Home, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand SearchTutorial = new RoutedUICommand("Туторијал претрага реда вожње", "Туторијал претрага реда вожње", typeof(ClientPage), new InputGestureCollection() { });
    }
}
