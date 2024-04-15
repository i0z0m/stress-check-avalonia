using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using StressCheckAvalonia.Models;

namespace StressCheckAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public AggregateResults AggregateResultsControl { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "FLOSS版ストレスチェック実施プログラム 標準版（57項目）";
            this.DataContext = SectionViewModel.Instance;

            // Instantiate EmployeeInformation control
            var employeeInformationControl = new EmployeeInformation();
            this.FindControl<ContentControl>("EmployeeInformationControl").Content = employeeInformationControl;

            // Set IsInput to true
            SectionViewModel.Instance.CurrentState = State.Input;
        }

        public void DisplayQuestions(int sectionIndex, int questionCount)
        {
            // Update the displayed questions in the SectionViewModel
            SectionViewModel.Instance.UpdateDisplayedQuestions(sectionIndex, questionCount);

            // Clear the existing questions
            QuestionsPanel.Children.Clear();

            // Add each question and its corresponding choice buttons to the QuestionsPanel
            foreach (var questionViewModel in SectionViewModel.Instance.DisplayedQuestionViewModels)
            {
                var questionText = new QuestionText
                {
                    DataContext = questionViewModel // Set the DataContext to the QuestionViewModel
                };

                var choiceButtons = new ChoiceButtons
                {
                    DataContext = questionViewModel, // Set the DataContext to the QuestionViewModel
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                    Margin = new Thickness(0, 0, 50, 0)
                };

                // Set the IsChecked property of the RadioButton corresponding to the selected choice
                var selectedChoice = questionViewModel.Question.Score;
                if (selectedChoice >= 1 && selectedChoice <= 4)
                {
                    var radioButton = choiceButtons.FindControl<RadioButton>($"RadioButton{selectedChoice}");
                    radioButton.IsChecked = true;
                    questionViewModel.HandleChoiceSelect(selectedChoice);
                }

                var questionGrid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition(1, GridUnitType.Star),
                        new ColumnDefinition(0, GridUnitType.Auto)
                    }
                };

                Grid.SetColumn(questionText, 0);
                Grid.SetColumn(choiceButtons, 1);

                questionGrid.Children.Add(questionText);
                questionGrid.Children.Add(choiceButtons);

                QuestionsPanel.Children.Add(questionGrid);

                var underline = new Border
                {
                    BorderThickness = new Thickness(0, 0, 0, 1),
                    BorderBrush = Brushes.Gray,
                    Margin = new Thickness(50, 0, 50, 10)
                };
                QuestionsPanel.Children.Add(underline);
            }
        }

        public void ShowResults()
        {
            // Initialize AggregateResults
            if (EmployeeViewModel.Instance?.Employee != null)
            {
                AggregateResultsControl = new AggregateResults();
                AggregateResultsControl.DisplayResults(EmployeeViewModel.Instance.Employee);

                // Set IsAggregaed to true
                SectionViewModel.Instance.CurrentState = State.Aggregated;

                // Show the AggregateResults
                ResultsContent.Content = AggregateResultsControl;
            }
        }
    }
}