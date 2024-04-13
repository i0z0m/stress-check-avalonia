using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;

namespace StressCheckAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public int QuestionStartIndex { get; set; }
        public int QuestionsPerPage { get; } = 10;
        public AggregateResults AggregateResultsControl { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "FLOSS版ストレスチェック実施プログラム 標準版（57項目）";

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
            var questions = SectionViewModel.Instance.Questions;

            // Clear the DisplayedQuestionViewModels list
            SectionViewModel.Instance.DisplayedQuestionViewModels.Clear();

            // Add each question and its corresponding choice buttons to the QuestionsPanel
            for (int i = QuestionStartIndex; i < QuestionStartIndex + QuestionsPerPage && i < questions.Count; i++)
            {
                if (i < SectionViewModel.Instance.QuestionViewModels.Count)
                {
                    var questionViewModel = SectionViewModel.Instance.QuestionViewModels[i];

                    // Add the question to the DisplayedQuestionViewModels list
                    SectionViewModel.Instance.DisplayedQuestionViewModels.Add(questionViewModel);

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
        }

        public bool IsEmployeeInformationComplete()
        {
            var employeeInformationControl = this.FindControl<ContentControl>("EmployeeInformationControl").Content as EmployeeInformation;
            return employeeInformationControl != null && employeeInformationControl.IsInformationComplete();
        }

        public bool AreAllQuestionsDisplayed()
        {
            var questions = SectionViewModel.Instance.Questions;
            return QuestionStartIndex + QuestionsPerPage >= questions.Count;
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