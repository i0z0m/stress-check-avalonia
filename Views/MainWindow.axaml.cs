using Avalonia.Controls;
using Avalonia;
using System.Linq;

namespace stress_check_avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "ストレスチェック実施プログラム";

            // Initialize the first section
            InitSections(0);
        }

        public void InitSections(int sectionIndex)
        {
            // Set the current section in the SectionViewModel
            SectionViewModel.Instance.SetCurrentSection(sectionIndex);

            // Clear the existing questions
            QuestionsPanel.Children.Clear();

            // Get the questions for the specified section
            var questions = SectionViewModel.Instance.Questions;

            // Add each question and its corresponding choice buttons to the QuestionsPanel
            for (int i = 0; i < questions.Count; i++)
            {
                var questionText = new QuestionText
                {
                    QuestionIndex = i
                };

                var choiceButtons = new ChoiceButtons
                {
                    QuestionIndex = i,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                    Margin = new Thickness(0, 0, 53, 0)
                };

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
            }
        }

        public bool AreAllQuestionsAnswered()
        {
            return QuestionsPanel.Children.OfType<Grid>()
                .SelectMany(grid => grid.Children.OfType<ChoiceButtons>())
                .All(choiceButtons => Enumerable.Range(1, 4)
                    .Any(i => choiceButtons.FindControl<RadioButton>($"RadioButton{i}").IsChecked == true));
        }
    }
}