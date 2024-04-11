using StressCheckAvalonia.Models;
using ReactiveUI;

namespace StressCheckAvalonia.ViewModels
{
    public class EmployeeViewModel : ReactiveObject
    {
        private static EmployeeViewModel _instance;
        public static EmployeeViewModel Instance => _instance ??= new EmployeeViewModel();

        private string _name;
        private string _id;
        // Add other properties from Employee class

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string ID
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        // Implement other properties in a similar way

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set
            {
                this.RaiseAndSetIfChanged(ref _employee, value);
                if (value != null)
                {
                    Name = value.Name;
                    ID = value.ID;
                    // Set other properties
                }
            }
        }

        private EmployeeViewModel()
        {
            Employee = new Employee();
        }

        public bool IsInformationComplete()
        {
            return true;
        }
    }
}
