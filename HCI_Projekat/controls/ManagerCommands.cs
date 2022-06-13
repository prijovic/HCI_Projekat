using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCI_Projekat.controls
{
    public static class ManagerCommands
    {
        public static readonly RoutedUICommand Schedule = new RoutedUICommand("Ред вожњи", "Ред вожњи", typeof(App), new InputGestureCollection() { new KeyGesture(Key.R, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand TrainLines = new RoutedUICommand("Возне линије", "Возне линије", typeof(App), new InputGestureCollection() { new KeyGesture(Key.L, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Trains = new RoutedUICommand("Возови", "Возови", typeof(App), new InputGestureCollection() { new KeyGesture(Key.V, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Network = new RoutedUICommand("Мрежа линија", "Мрежа линија", typeof(App), new InputGestureCollection() { new KeyGesture(Key.M, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Logout = new RoutedUICommand("Одјави се", "Одјави се", typeof(App), new InputGestureCollection() { new KeyGesture(Key.Escape, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Home = new RoutedUICommand("Почетна страница", "Почетна страница", typeof(App), new InputGestureCollection() { new KeyGesture(Key.Home, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand TicketsMonth = new RoutedUICommand("Карте месеца", "Карте месеца", typeof(App), new InputGestureCollection() { new KeyGesture(Key.K, ModifierKeys.Control) });
        public static readonly RoutedUICommand TicketsRide = new RoutedUICommand("Карте вожње", "Карте вожње", typeof(App), new InputGestureCollection() { new KeyGesture(Key.K, ModifierKeys.Control | ModifierKeys.Alt) });
    }
}
