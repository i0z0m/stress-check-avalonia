using System;
using ReactiveUI;

namespace stress_check_avalonia
{
    public class EmployeeViewModel : ReactiveObject
    {
        private static EmployeeViewModel _instance;
        public static EmployeeViewModel Instance => _instance ??= new EmployeeViewModel();

        private Employee _employee;

        public Employee Employee
        {
            get => _employee;
            set => this.RaiseAndSetIfChanged(ref _employee, value);
        }

        private EmployeeViewModel()
        {
            Employee = new Employee();
        }
    }
}