using HCI_Projekat.controls.pages;
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
        public static readonly RoutedUICommand Schedule = new RoutedUICommand("Ред вожњи", "Ред вожњи", typeof(ManagerPage), new InputGestureCollection() { new KeyGesture(Key.R, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand TrainLines = new RoutedUICommand("Возне линије", "Возне линије", typeof(ManagerPage), new InputGestureCollection() { new KeyGesture(Key.L, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Trains = new RoutedUICommand("Возови", "Возови", typeof(ManagerPage), new InputGestureCollection() { new KeyGesture(Key.V, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Network = new RoutedUICommand("Мрежа линија", "Мрежа линија", typeof(ManagerPage), new InputGestureCollection() { new KeyGesture(Key.M, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Logout = new RoutedUICommand("Одјави се", "Одјави се", typeof(ManagerPage), new InputGestureCollection() { new KeyGesture(Key.Escape, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand Home = new RoutedUICommand("Почетна страница", "Почетна страница", typeof(ManagerPage), new InputGestureCollection() { new KeyGesture(Key.Home, ModifierKeys.Control | ModifierKeys.Alt) });
        public static readonly RoutedUICommand TrainTutorial = new RoutedUICommand("Туторијал воз", "Туторијал воз", typeof(ManagerPage), new InputGestureCollection() {});
        public static readonly RoutedUICommand TrainLineTutorial = new RoutedUICommand("Туторијал линија", "Туторијал линија", typeof(ManagerPage), new InputGestureCollection() { });
        public static readonly RoutedUICommand ScheduleItemTutorial = new RoutedUICommand("Туторијал ред вожње", "Туторијал ред вожње", typeof(ManagerPage), new InputGestureCollection() { });
    }
}
