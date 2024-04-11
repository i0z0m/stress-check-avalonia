using ReactiveUI;
using StressCheckAvalonia.Models;
using System;

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
            _employee = new Employee();
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
                }
            }
        }

        public string Level
        {
            get => _employee?.Level;
            set
            {
                if (_employee != null)
                {
                    _employee.Level = value;
                    this.RaisePropertyChanged();
                }
            }
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
                }
            }
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
                }
            }
        }

        public DateTime Birthday
        {
            get => _employee?.Birthday ?? default;
            set
            {
                if (_employee != null)
                {
                    _employee.Birthday = value;
                    this.RaisePropertyChanged();
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
                }
            }
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
                }
            }
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
            return true;
        }
    }
}