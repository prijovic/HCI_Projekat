using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        private readonly UserType _userType;

        public User()
        {

        }

        public User(string name, string surname, string username, string password, UserType userType)
        {
            this._name = name;
            this._surname = surname;
            this._username = username;
            this._password = password;
            this._userType = userType;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (value != _surname)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string UserType
        {
            get
            {
                return _userType.ToString();
            }
        }
    }
}
