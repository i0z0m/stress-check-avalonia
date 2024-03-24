using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System;

namespace stress_check_avalonia
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
                radioButton.IsCheckedChanged += OnChoiceSelect;
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
        public void OnChoiceSelect(object? sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.IsChecked == true)
            {
                var choiceIndex = int.Parse(radioButton.Tag.ToString());
                // Update the Score property of the Question
                QuestionViewModel.Question.Score = choiceIndex;

                var groupName = radioButton.GroupName;

                System.Diagnostics.Debug.WriteLine($"GroupName is: {groupName}");
                System.Diagnostics.Debug.WriteLine($"ViewModel is: {DataContext as SectionViewModel}");
                System.Diagnostics.Debug.WriteLine($"Selected choice: {choiceIndex}");

                (DataContext as SectionViewModel)?.HandleChoiceSelect(choiceIndex.ToString(), groupName);
            }
        }
    }
}