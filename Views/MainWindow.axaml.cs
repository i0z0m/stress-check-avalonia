using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;

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
            SectionViewModel.Instance.IsInput = true;
        }

        public void DisplayQuestions(int sectionIndex, int questionCount)
        {
            // Set the current section in the SectionViewModel
            SectionViewModel.Instance.SetCurrentSection(sectionIndex);

            // Clear the existing questions
            QuestionsPanel.Children.Clear();

            // Get the questions for the specified section
            var displayedQuestionViewModels = SectionViewModel.Instance.GetDisplayedQuestions();

            // Add each question and its corresponding choice buttons to the QuestionsPanel
            foreach (var questionViewModel in displayedQuestionViewModels)
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

                // Hide the QuestionsPanel
                QuestionsPanel.IsVisible = false;

                // Set IsAggregated to true
                SectionViewModel.Instance.IsAggregated = true;

                // Show the AggregateResults
                ResultsContent.Content = AggregateResultsControl;
            }
        }
    }
}