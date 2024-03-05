using Avalonia.Controls;
using Avalonia;

namespace stress_check_avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "ストレスチェック実施プログラム";

            // Use the Questions from SectionViewModel
            var questions = SectionViewModel.Instance.Questions;

            for (int i = 0; i < questions.Count; i++)
            {
                var questionText = new QuestionText
                {
                    QuestionIndex = i
                };

                var choiceButtons = new ChoiceButtons
                {
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
    }
}