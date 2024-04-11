using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views
{
    public partial class EmployeeInformation : UserControl
    {
        public EmployeeViewModel? ViewModel { get; }

        public EmployeeInformation()
        {
            InitializeComponent();
            this.WhenAnyValue(x => x.DataContext).BindTo(this, x => x.ViewModel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
