using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.ViewModels;
using Avalonia.Media;

namespace StressCheckAvalonia.Views
{
    public partial class EmployeeInformation : UserControl
    {
        public EmployeeInformation()
        {
            InitializeComponent();
            DataContext = EmployeeViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public bool IsInformationComplete()
        {
            return ((EmployeeViewModel)DataContext).IsInformationComplete();
        }
    }
}