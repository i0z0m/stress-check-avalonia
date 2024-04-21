using ReactiveUI;
using StressCheckAvalonia.Models;
using System;
using Avalonia.Media;
using System.Linq;

namespace StressCheckAvalonia.ViewModels
{
    public class EmployeeViewModel : ReactiveObject
    {
        private static EmployeeViewModel? _instance;
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
            _level = string.Empty;
            _levelText = string.Empty;
            _levelColor = Brushes.Transparent;
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

        public string? Gender
        {
            get => _employee.Gender;
            set
            {
                if (_employee.Gender != value)
                {
                    _employee.Gender = value ?? string.Empty;
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

        public string? Name
        {
            get => _employee.Name;
            set
            {
                if (_employee.Name != value)
                {
                    _employee.Name = value ?? string.Empty;
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

        private string ConvertToKatakana(string input)
        {
            // Ensure only Katakana is allowed, or convert from Hiragana to Katakana
            // This is just an example conversion; adjust according to your exact requirements
            return string.Concat(input.Select(c =>
                (c >= 'ぁ' && c <= 'ん') ? (char)(c - 'ぁ' + 'ァ') : c));
        }

        public string? Furigana
        {
            get => _employee.Furigana;
            set
            {
                var katakanaValue = ConvertToKatakana(value ?? string.Empty);
                if (katakanaValue.All(c => (c >= 'ァ' && c <= 'ヶ') || char.IsWhiteSpace(c) || (c >= 'ー' && c <= 'ヿ')))
                {
                    _employee.Furigana = katakanaValue;
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
            get => _employee.Birthday;
            set
            {
                if (_employee.Birthday != value)
                {
                    _employee.Birthday = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string? ID
        {
            get => _employee.ID;
            set
            {
                if (_employee.ID != value)
                {
                    _employee.ID = value ?? string.Empty;
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

        public string? Workplace
        {
            get => _employee.Workplace;
            set
            {
                if (_employee.Workplace != value)
                {
                    _employee.Workplace = value ?? string.Empty;
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

        public string? Email
        {
            get => _employee.Email;
            set
            {
                if (_employee.Email != value)
                {
                    _employee.Email = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string? Phone1
        {
            get => _employee.Phone1;
            set
            {
                if (value != null && value.All(c => c >= '0' && c <= '9') && value.Length <= 4)
                {
                    _employee.Phone1 = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string? Phone2
        {
            get => _employee.Phone2;
            set
            {
                if (value != null && value.All(c => c >= '0' && c <= '9') && value.Length <= 4)
                {
                    _employee.Phone2 = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string? Phone3
        {
            get => _employee.Phone3;
            set
            {
                if (value != null && value.All(c => c >= '0' && c <= '9') && value.Length <= 4)
                {
                    _employee.Phone3 = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string? Extension
        {
            get => _employee.Extension;
            set
            {
                if (value != null && value.All(c => c >= '0' && c <= '9'))
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
            var errorBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
            if (string.IsNullOrWhiteSpace(Name))
                NameBackground = errorBrush;
            if (string.IsNullOrWhiteSpace(Furigana))
                FuriganaBackground = errorBrush;
            if (string.IsNullOrEmpty(Gender))
                GenderBackground = errorBrush;
            if (string.IsNullOrWhiteSpace(ID))
                IDBackground = errorBrush;
            if (string.IsNullOrWhiteSpace(Workplace))
                WorkplaceBackground = errorBrush;
        }
    }
}