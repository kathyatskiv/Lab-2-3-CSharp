using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using YatskivLab02.Properties;
using YatskivLab02.Models;

namespace YatskivLab02.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {

        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday = new DateTime(2001, 1, 21);
        private string _resultMessage;

        private RelayCommand<object> _proceedCommand;
        #endregion


        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public bool IsBirthday {
            get
            {
                return DateTime.Today.Month == _birthday.Month && DateTime.Today.Day == _birthday.Day;
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

        #region Comands
        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ??
            (_proceedCommand = new RelayCommand<object>(ProceedExecute, ProceedCanExecute));

        private bool ProceedCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(_name) &&
                   !string.IsNullOrEmpty(_surname) &&
                   !string.IsNullOrEmpty(_email);
        }

        private async void ProceedExecute(object obj)
        {
           
            var result = await Task.Run(() =>
            {
                Person person;
                try
                {
                 
                    person = new Person(_name, _surname, _email, _birthday);

                    string adulty = person.IsAdult ? "adult" : "not adult";
                    string hb = person.IsBirthday ? "Happy Birthday!" : "";
                    ResultMessage = $"{Name} {Surname}, {Email}, {Birthday.ToLongDateString()}" +
                    $"Person is {adulty}, Sun sign is {person.SunSign}, Chinese sign is {person.ChineseSign}";
                }
                catch (PersonException ex)
                {
                    MessageBox.Show(ex.Message);
                    ResultMessage = "Inncorect data!";
                    return false;
                }
              
                if (person.IsBirthday)
                {
                    MessageBox.Show($"Happy birthday, {_name}");
                }

                return true;
            });
            
        }
        #endregion

       
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
