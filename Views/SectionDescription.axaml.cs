using StressCheckAvalonia.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StressCheckAvalonia.Views
{
    public partial class SectionDescription : UserControl
    {
        public static readonly AvaloniaProperty<string> DescriptionProperty =
            AvaloniaProperty.Register<SectionDescription, string>(nameof(Description));

        public static readonly AvaloniaProperty<bool> IsLastSectionProperty =
            AvaloniaProperty.Register<SectionDescription, bool>(nameof(IsLastSection));

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public bool IsLastSection
        {
            get => (bool)GetValue(IsLastSectionProperty);
            set => SetValue(IsLastSectionProperty, value);
        }

        public SectionDescription()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.Property == DescriptionProperty && DescriptionTextBlock != null)
            {
                DescriptionTextBlock.Text = Description;
            }

            if (args.Property == IsLastSectionProperty)
            {
            }
        }
    }
}