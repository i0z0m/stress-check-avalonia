﻿using StressCheckAvalonia.Models;
using ReactiveUI;

namespace StressCheckAvalonia.ViewModels
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