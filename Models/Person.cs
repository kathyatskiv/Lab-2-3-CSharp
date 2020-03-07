using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatskivLab02.Models
{
    internal class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        private int _age;
        private string _sunSign;
        private string _chineseSign;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
            }
        }

        public bool IsAdult
        {
            get { return _age >= 18; }
        }
           
        public bool IsBirthday
        {
            get
            {
                DateTime now = DateTime.Today;
                return Birthday.Month == now.Month && Birthday.Day == now.Day;
            }
        }

        public bool IsAgeCorrect
        {
            get { return _age > 0; }
        }

        public string SunSign
        {
            get { return _sunSign; }
            private set
            {
                _sunSign = value;
            }
        }
        #endregion

        #region Constructors
        internal Person()
        {
            Name = "";
            Surname = "";
            Email = "";
            Birthday = DateTime.Today;

            compute();
        }

        internal Person(string name, string surname, string email, DateTime birthday )
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;

            compute();
        }

        internal Person(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = DateTime.Today;

            compute();
        }

        internal Person(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Email = null;
            Birthday = birthday;

            compute();
        }
        #endregion

        private void compute()
        {
            countAge();
            setSunSign();
        }

        private void countAge()
        {
            DateTime now = DateTime.Today;

           _age = now.Month > Birthday.Month ? now.Year - Birthday.Year
                   : now.Month == Birthday.Month && now.Day >= Birthday.Day ? now.Year - Birthday.Year
                   : now.Year - Birthday.Year - 1;

            if (_age < 0 || _age > 135) _age = -1;
        }

        private enum WestZodiac
        {
            Capricornus = 27,
            Aquarius = 24,
            Pisces = 38,
            Aries = 25,
            Taurus = 37,
            Gemini = 31,
            Cancer = 20,
            Leo = 37,
            Virgo = 45,
            Libra = 7,
            Sagittarius = 32
        }

        private void setSunSign()
        {
            int cur = 19;
            int counter = 0;

            foreach(int zodiac in Enum.GetValues(typeof(WestZodiac)))
            {
                if (cur >= Birthday.DayOfYear) SunSign = (Enum.GetNames(typeof(WestZodiac)))[counter];
                cur += zodiac;
                counter++;
            }
        }
    }
}
