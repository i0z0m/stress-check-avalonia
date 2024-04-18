using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Globalization;

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
                radioButton.IsCheckedChanged += (sender, e) => {
                    if (sender is RadioButton rb && rb.Tag != null)
                    {
                        var tagString = rb.Tag.ToString();
                        if (int.TryParse(tagString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int choiceValue))
                        {
                            HandleChoiceSelect(choiceValue);
                        }
                    }
                };
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
