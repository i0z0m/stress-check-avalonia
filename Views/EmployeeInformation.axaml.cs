using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.ViewModels;
using System;
using System;

namespace StressCheckAvalonia.Views
{
    public partial class EmployeeInformation : UserControl
    {
        public EmployeeInformation()
        {
            InitializeComponent();
            DataContext = EmployeeViewModel.Instance;

            DatePicker datePicker = this.FindControl<DatePicker>("BirthdayDatePicker");
            // datePicker.SelectedDate = new DateTimeOffset(DateTime.Today);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
