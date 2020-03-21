using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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


        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new NullPersonException();

                Regex regex = new Regex(@"^[a-zA-Z'-]+$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    _name = value;
                }
                else
                    throw new InvalidNameException(value);
               
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new NullPersonException();

                Regex regex = new Regex(@"^[a-zA-Z'-]+$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    _surname = value;
                }
                else
                    throw new InvalidSurnameException(value);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new NullPersonException();

                Regex regex = new Regex(@"^(\w+)+@(\w+)(\.)(\w+)$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    _email = value;
                }
                else
                    throw new InvalidEmailException(value);
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            { 
                int _age = (DateTime.Today.Year - value.Year) -
                    (DateTime.Today.DayOfYear >= value.DayOfYear ? 0 : 1);

                if (_age > 135) throw new DeadPersonException(value);
                else if (_age < 0) throw new NotBornPersonException(value);
                else _birthday = value;


            }
        }

        public bool IsAdult
        {
            get
            {
                return (DateTime.Today.Year - _birthday.Year) -
                               (DateTime.Today.DayOfYear >= _birthday.DayOfYear ? 0 : 1) >= 18;
            }
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
            get
            {
                int counter = 0;

                foreach (int zodiac in Enum.GetValues(typeof(WestZodiac)))
                {
                    if (zodiac >= Birthday.DayOfYear)
                    {
                        SunSign = (Enum.GetNames(typeof(WestZodiac)))[counter];
                        break;
                    }

                    counter++;
                }

                return _sunSign;
            }
            private set
            {
                _sunSign = value;
            }
        }

        public string ChineseSign
        {
            get
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

                return _chineseSign;
            }
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

        }

        internal Person(string name, string surname, string email, DateTime birthday )
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;

        }

        public Person(string firstName, string lastName, string email) : this(firstName, lastName, email,
            new DateTime(2001, 1, 1))
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName, " ", birthDate)
        {
        }

        #endregion

    }

    #region Exceptions

    public class PersonException : Exception
    {
        public PersonException(string message)
            : base(message)
        {
        }
    }

    public class NullPersonException : PersonException
    {
        public NullPersonException()
            : base($"Person does not exist!")
        {
        }
    }

    public class InvalidEmailException : PersonException
    {
        public InvalidEmailException(string email)
            : base($"Email {email} is not valid!")
        {
        }
    }

    public class InvalidNameException : PersonException
    {
        public InvalidNameException(string name)
            : base($"{name} is not valid name")
        {
        }
    }

    public class InvalidSurnameException : PersonException
    {
        public InvalidSurnameException(string surname)
            : base($"{surname} is not valid surname")
        {
        }
    }

    public class NotBornPersonException : PersonException
    {
        public NotBornPersonException(DateTime birthday)
            : base($"{birthday.ToShortDateString()} is in the Future!")
        {
        }
    }

    public class DeadPersonException : PersonException
    {
        public DeadPersonException(DateTime birthday)
            : base($"{birthday.ToShortDateString()} was more than 135 years ago!")
        {
        }
    }

    #endregion
}
