using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views
{
    public partial class AppHeader : UserControl
    {
        public AppHeader()
        {
            InitializeComponent();
            DataContext = EmployeeViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
