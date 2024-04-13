using StressCheckAvalonia.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System;

namespace StressCheckAvalonia.Views
{
    public partial class ChoiceButtons : UserControl
    {
        public static readonly AvaloniaProperty QuestionIndexProperty =
    AvaloniaProperty.Register<ChoiceButtons, int>("QuestionIndex", defaultBindingMode: BindingMode.TwoWay);

        public QuestionViewModel QuestionViewModel { get; set; }

        public int QuestionIndex
        {
            get { return (int)(GetValue(QuestionIndexProperty) ?? 0); } // Cast to int and handle possible null value
            set
            {
                SetValue(QuestionIndexProperty, value);
                if (DataContext is SectionViewModel viewModel)
                {
                    viewModel.QuestionIndex = value;
                }
                UpdateGroupName();
            }
        }

        public ChoiceButtons()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
            this.WhenAnyValue(x => x.QuestionIndex).Subscribe(_ => UpdateGroupName());
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
                radioButton.Checked += (sender, e) => QuestionViewModel.HandleChoiceSelect(int.Parse(radioButton.Tag.ToString()));
            }
        }

        private void UpdateGroupName()
        {
            // Update the GroupName of each RadioButton based on the new QuestionIndex
            UpdateGroupNameForRadioButton("RadioButton1");
            UpdateGroupNameForRadioButton("RadioButton2");
            UpdateGroupNameForRadioButton("RadioButton3");
            UpdateGroupNameForRadioButton("RadioButton4");
        }

        private void UpdateGroupNameForRadioButton(string radioButtonName)
        {
            var radioButton = this.FindControl<RadioButton>(radioButtonName);
            if (radioButton != null)
            {
                radioButton.GroupName = QuestionIndex.ToString();
            }
        }
    }
}