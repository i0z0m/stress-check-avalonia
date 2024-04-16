using ReactiveUI;
using StressCheckAvalonia.Models;
using System;
using Avalonia.Media;

namespace StressCheckAvalonia.ViewModels
{
    public class EmployeeViewModel : ReactiveObject
    {
        private static EmployeeViewModel _instance;
        public static EmployeeViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmployeeViewModel();
                }
                return _instance;
            }
        }
        private EmployeeViewModel()
        {
            _employee = new Employee { Birthday = new DateTime(2000, 1, 1) };
        }

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set
            {
                System.Diagnostics.Debug.WriteLine($"Setting Employee to {value}");
                this.RaiseAndSetIfChanged(ref _employee, value);
            }
        }

        public string Gender
        {
            get => _employee?.Gender;
            set
            {
                if (_employee != null)
                {
                    _employee.Gender = value;
                    this.RaisePropertyChanged();
                    GenderBackground = Brushes.White;
                }
            }
        }

        private IBrush _genderBackground = Brushes.White;
        public IBrush GenderBackground
        {
            get => _genderBackground;
            private set => this.RaiseAndSetIfChanged(ref _genderBackground, value);
        }

        private string _level;
        public string Level
        {
            get => _level;
            set
            {
                this.RaiseAndSetIfChanged(ref _level, value);
                LevelText = _level == "High" ? "高ストレス者です" : "低ストレス者です";
                LevelColor = _level == "High" ? new SolidColorBrush(Color.FromArgb(128, 255, 0, 0)) : new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));
            }
        }

        private string _levelText;

        public string LevelText
        {
            get => _levelText;
            private set => this.RaiseAndSetIfChanged(ref _levelText, value);
        }

        private IBrush _levelColor;
        public IBrush LevelColor
        {
            get => _levelColor;
            private set => this.RaiseAndSetIfChanged(ref _levelColor, value);
        }

        public string Name
        {
            get => _employee?.Name;
            set
            {
                if (_employee != null)
                {
                    _employee.Name = value;
                    this.RaisePropertyChanged();
                    NameBackground = Brushes.White;
                }
            }
        }

        private IBrush _nameBackground = Brushes.White;
        public IBrush NameBackground
        {
            get => _nameBackground;
            private set => this.RaiseAndSetIfChanged(ref _nameBackground, value);
        }

        public string Furigana
        {
            get => _employee?.Furigana;
            set
            {
                if (_employee != null)
                {
                    _employee.Furigana = value;
                    this.RaisePropertyChanged();
                    FuriganaBackground = Brushes.White;
                }
            }
        }

        private IBrush _furiganaBackground = Brushes.White;
        public IBrush FuriganaBackground
        {
            get => _furiganaBackground;
            private set => this.RaiseAndSetIfChanged(ref _furiganaBackground, value);
        }

        public DateTimeOffset Birthday
        {
            get => _employee?.Birthday ?? default;
            set
            {
                if (_employee != null)
                {
                    _employee.Birthday = value;
                    this.RaisePropertyChanged();
                }
                else
                {
                    throw new NullReferenceException("_employee is null");
                }
            }
        }

        public string ID
        {
            get => _employee?.ID;
            set
            {
                if (_employee != null)
                {
                    _employee.ID = value;
                    this.RaisePropertyChanged();
                    IDBackground = Brushes.White;
                }
            }
        }

        private IBrush _idBackground = Brushes.White;
        public IBrush IDBackground
        {
            get => _idBackground;
            private set => this.RaiseAndSetIfChanged(ref _idBackground, value);
        }

        public string Workplace
        {
            get => _employee?.Workplace;
            set
            {
                if (_employee != null)
                {
                    _employee.Workplace = value;
                    this.RaisePropertyChanged();
                    WorkplaceBackground = Brushes.White;
                }
            }
        }

        private IBrush _workplaceBackground = Brushes.White;
        public IBrush WorkplaceBackground
        {
            get => _workplaceBackground;
            private set => this.RaiseAndSetIfChanged(ref _workplaceBackground, value);
        }

        public string Email
        {
            get => _employee?.Email;
            set
            {
                if (_employee != null)
                {
                    _employee.Email = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string Phone
        {
            get => _employee?.Phone;
            set
            {
                if (_employee != null)
                {
                    _employee.Phone = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string Extension
        {
            get => _employee?.Extension;
            set
            {
                if (_employee != null)
                {
                    _employee.Extension = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public bool IsInformationComplete()
        {
            return !string.IsNullOrEmpty(Gender) &&
                   !string.IsNullOrEmpty(Name) &&
                   !string.IsNullOrEmpty(Furigana) &&
                   !string.IsNullOrEmpty(ID) &&
                   !string.IsNullOrEmpty(Workplace);
        }

        public void ValidateInput()
        {
            // Create a SolidColorBrush with the desired color
            var errorBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

            // Check each required field
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameBackground = errorBrush;
            }
            if (string.IsNullOrWhiteSpace(Furigana))
            {
                FuriganaBackground = errorBrush;
            }
            if (string.IsNullOrEmpty(Gender))
            {
                GenderBackground = errorBrush;
            }
            if (string.IsNullOrWhiteSpace(ID))
            {
                IDBackground = errorBrush;
            }
            if (string.IsNullOrWhiteSpace(Workplace))
            {
                WorkplaceBackground = errorBrush;
            }
        }
    }
}