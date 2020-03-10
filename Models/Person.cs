using System;
using System.Windows;
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

                try
                {
                    countAge();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
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

        public string ChineseSign
        {
            get { return _chineseSign; }
            private set
            {
                _chineseSign = value;
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

            countAge();

        }

        internal Person(string name, string surname, string email, DateTime birthday )
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;

            countAge();
        }

        internal Person(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;

            countAge();
        }

        internal Person(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Email = null;
            Birthday = birthday;

            countAge();
        }
        #endregion

        async private void compute()
        {
            await Task.Run(() =>
            {
                try
                {
                    countAge();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    setSunSign();
                    setChineseSign();
                }
        
            });

        }

        private void countAge()
        {
            DateTime now = DateTime.Today;

            _age = now.Month > Birthday.Month ? now.Year - Birthday.Year
                   : now.Month == Birthday.Month && now.Day >= Birthday.Day ? now.Year - Birthday.Year
                   : now.Year - Birthday.Year - 1;

            if (_age > 135 || _age < 0) throw new ArgumentException("Error! Wrong date");
        }

        private enum WestZodiac
        {
            Capricornus = 19,
            Aquarius = 47,
            Pisces = 71,
            Aries = 109,
            Taurus = 134,
            Gemini = 171,
            Cancer = 202,
            Leo = 222,
            Virgo = 259,
            Libra = 205,
            Scorpio = 327,
            Sagittarius = 352
        }

        private enum ChineseZodiac
        {
            Monkey = 0,
            Cock,
            Dog,
            Pig,
            Rat,
            Ox,
            Tiger,
            Heir,
            Dragon,
            Snake,
            Horse,
            Goal
        }

    private void setSunSign()
        {
            int counter = 0;

            foreach(int zodiac in Enum.GetValues(typeof(WestZodiac)))
            {
                if (zodiac >= Birthday.DayOfYear)
                {
                    SunSign = (Enum.GetNames(typeof(WestZodiac)))[counter];
                    break;
                }

                counter++;
            }
        }

        private void setChineseSign()
        {
            int counter = 0;

            foreach (int zodiac in Enum.GetValues(typeof(ChineseZodiac)))
            {
                if (Birthday.Year % 12 == zodiac)
                {
                    ChineseSign = (Enum.GetNames(typeof(ChineseZodiac)))[counter];
                    break;
                }

                counter++;
            }
        }

    }
}
