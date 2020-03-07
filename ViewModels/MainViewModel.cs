using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using YatskivLab02.Properties;
using YatskivLab02.Models;

namespace YatskivLab02.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person;
        private RelayCommand<object> _proceedCommand;
        private string _resultMessage;
        #endregion

        #region Properties
        public string Name
        {
            get { return _person.Name; }
            set
            {
                _person.Name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _person.Surname; }
            set
            {
                _person.Surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _person.Email; }
            set
            {
                _person.Email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birtday
        {
            get { return _person.Birthday; }
            set
            {
                _person.Birthday = value;
                OnPropertyChanged();
            }
        }

        public string ResultMessage
        {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public MainViewModel()
        {
            _person = new Person();

        }

        public RelayCommand<object> ProceedCommand
        { 
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(
                           o =>
                           {
                               MessageBox.Show($"Succes");

                               string adulty = _person.IsAdult ? "adult" : "not adult";
                               string hb = _person.IsBirthday ? "Happy Birthday!" : "";
                               ResultMessage = $"{Name} {Surname}, {Email}, {Birtday.ToLongDateString()}\nPerson is {adulty}, SunSign is {_person.SunSign}\n{hb}";

                           }, o => CanExecuteCommand()));
            }
        }

        public bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_person.Name) && !string.IsNullOrWhiteSpace(_person.Surname) && !string.IsNullOrWhiteSpace(_person.Email) && !_person.IsAgeCorrect;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
