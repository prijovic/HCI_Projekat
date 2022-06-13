using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat.model
{
    public class Train : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _name;
        private string _code;
        private int _capacity;

        public Train(string name, string code, int capacity)
        {
            this._name = name;
            this._code = code;
            this._capacity = capacity;
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

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (value != _code)
                {
                    _code = value;
                    OnPropertyChanged("Code");
                }
            }
        }

        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                if (value != _capacity)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        public override string ToString()
        {
            return Code + " " + Name;
        }
    }
}
