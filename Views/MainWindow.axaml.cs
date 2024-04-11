using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;
using System.Linq;

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
            this.Title = "ストレスチェック実施プログラム";

            var employeeInformationControl = new EmployeeInformation
            {
                DataContext = EmployeeViewModel.Instance // Use the Employee instance from EmployeeViewModel
            };
            this.FindControl<ContentControl>("EmployeeInformationControl").Content = employeeInformationControl;

            // Set DataContext for AppHeader
            var appHeaderControl = this.FindControl<AppHeader>("AppHeaderControl");
            appHeaderControl.DataContext = EmployeeViewModel.Instance;
        }


        public void DisplayQuestions(int sectionIndex, int questionCount)
        {
            // Set the current section in the SectionViewModel
            SectionViewModel.Instance.SetCurrentSection(sectionIndex);

            // Clear the existing questions
            QuestionsPanel.Children.Clear();

            // Get the questions for the specified section
            var questions = SectionViewModel.Instance.Questions;

            // Add each question and its corresponding choice buttons to the QuestionsPanel
            for (int i = QuestionStartIndex; i < QuestionStartIndex + QuestionsPerPage && i < questions.Count; i++)
            {
                if (i < SectionViewModel.Instance.QuestionViewModels.Count)
                {
                    var questionViewModel = SectionViewModel.Instance.QuestionViewModels[i];

                    var questionText = new QuestionText
                    {
                        QuestionIndex = i
                    };

                    var choiceButtons = new ChoiceButtons
                    {
                        QuestionViewModel = questionViewModel, // Pass the QuestionViewModel to the ChoiceButtons
                        QuestionIndex = i,
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 53, 0)
                    };

                    // Set the IsChecked property of the RadioButton corresponding to the selected choice
                    var selectedChoice = questionViewModel.Question.Score;
                    if (selectedChoice >= 1 && selectedChoice <= 4)
                    {
                        var radioButton = choiceButtons.FindControl<RadioButton>($"RadioButton{selectedChoice}");
                        radioButton.IsCheckedChanged -= choiceButtons.OnChoiceSelect;
                        radioButton.IsChecked = true;
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
                        Margin = new Thickness(48, 0, 48, 8)
                    };
                    QuestionsPanel.Children.Add(underline);
                }
            }
        }

        public bool IsEmployeeInformationComplete()
        {
            var employeeInformationControl = this.FindControl<ContentControl>("EmployeeInformationControl").Content as EmployeeInformation;
            return employeeInformationControl != null && EmployeeViewModel.Instance.IsInformationComplete();
        }

        public bool AreAllQuestionsAnswered()
        {
            return !QuestionsPanel.Children.OfType<Grid>()
                .SelectMany(grid => grid.Children.OfType<ChoiceButtons>())
                .Any(choiceButtons => Enumerable.Range(1, 4)
                    .All(i => choiceButtons.FindControl<RadioButton>($"RadioButton{i}").IsChecked != true));
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

                // Hide the SectionDescription
                SectionDescriptionControl.IsVisible = false;

                // Show the AggregateResults
                ResultsContent.Content = AggregateResultsControl;
            }
        }
    }
}