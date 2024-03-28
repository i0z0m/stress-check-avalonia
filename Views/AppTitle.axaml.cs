using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
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
