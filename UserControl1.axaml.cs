using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}