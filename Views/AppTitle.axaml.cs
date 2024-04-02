using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StressCheckAvalonia.Views
{
    public partial class AppTitle : UserControl
    {
        public AppTitle()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
