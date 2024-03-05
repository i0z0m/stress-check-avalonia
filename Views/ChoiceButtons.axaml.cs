using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace stress_check_avalonia
{
    public partial class ChoiceButtons : UserControl
    {
        public ChoiceButtons()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.FindControl<RadioButton>("RadioButton1").Checked += OnChoiceSelect;
            this.FindControl<RadioButton>("RadioButton2").Checked += OnChoiceSelect;
            this.FindControl<RadioButton>("RadioButton3").Checked += OnChoiceSelect;
            this.FindControl<RadioButton>("RadioButton4").Checked += OnChoiceSelect;
        }

        public void OnChoiceSelect(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            var choice = (string)radioButton.Content;

            var viewModel = DataContext as SectionViewModel;
            System.Diagnostics.Debug.WriteLine($"ViewModel is: {viewModel}");
            if (viewModel != null)
            {
                System.Diagnostics.Debug.WriteLine($"Selected choice: {choice}");

                viewModel.HandleChoiceSelect(choice);
            }
        }
    }
}