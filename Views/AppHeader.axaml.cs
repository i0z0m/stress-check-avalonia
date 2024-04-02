using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StressCheckAvalonia.Views
{
    public partial class AppHeader : UserControl
    {
        public AppHeader()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

