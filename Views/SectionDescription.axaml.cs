using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.Views
{
    public partial class SectionDescription : UserControl
    {
        public SectionDescription()
        {
            InitializeComponent();
            DataContext = StateViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
