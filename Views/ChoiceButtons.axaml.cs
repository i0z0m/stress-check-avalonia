using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StressCheckAvalonia.Views
{
    public partial class ChoiceButtons : UserControl
    {
        public ChoiceButtons()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            SubscribeToIsCheckedChanged("RadioButton1");
            SubscribeToIsCheckedChanged("RadioButton2");
            SubscribeToIsCheckedChanged("RadioButton3");
            SubscribeToIsCheckedChanged("RadioButton4");
        }

        private void SubscribeToIsCheckedChanged(string radioButtonName)
        {
            var radioButton = this.FindControl<RadioButton>(radioButtonName);
            if (radioButton != null)
            {
                radioButton.Checked += (sender, e) => HandleChoiceSelect(int.Parse(radioButton.Tag.ToString()));
            }
        }

        private void HandleChoiceSelect(int choiceValue)
        {
            if (DataContext is QuestionViewModel viewModel)
            {
                viewModel.HandleChoiceSelect(choiceValue);
            }
        }
    }
}
