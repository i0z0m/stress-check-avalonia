using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            DataContext = new SectionViewModel
            {
                Section = LoadSections.sections[0]
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
