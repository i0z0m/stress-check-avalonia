using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}