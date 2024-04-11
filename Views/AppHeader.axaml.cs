using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views
{
    public partial class AppHeader : UserControl
    {
        public EmployeeViewModel EmployeeViewModel { get; private set; }

        public AppHeader()
        {
            InitializeComponent();
            this.WhenAnyValue(x => x.DataContext).BindTo(this, x => x.EmployeeViewModel);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
