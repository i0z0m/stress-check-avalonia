using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace stress_check_avalonia
{
    public partial class BackButtons : UserControl
    {
        public BackButtons()
        {
            InitializeComponent();
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();
            if (mainWindow != null)
            {
                int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);

                if (mainWindow.QuestionStartIndex == 0)
                {
                    if (currentIndex > 0) // Check if it's not the first section
                    {
                        // Calculate the score of the current section
                        int sectionScore = SectionViewModel.Instance.Questions.CalculateScore();

                        // Output the section score to the console for debugging
                        System.Diagnostics.Debug.WriteLine($"Section Score: {sectionScore}");

                        // Decrement the section index
                        currentIndex--;

                        // Set the question start index to the first question of the last page of the previous section
                        var previousSectionQuestionCount = LoadSections.sections[currentIndex].Questions.Count;
                        mainWindow.QuestionStartIndex = ((previousSectionQuestionCount - 1) / mainWindow.QuestionsPerPage) * mainWindow.QuestionsPerPage;
                    }
                }
                else
                {
                    // Update the question start index
                    mainWindow.QuestionStartIndex -= mainWindow.QuestionsPerPage;
                }

                // Load previous section or page
                mainWindow.InitSections(currentIndex, mainWindow.QuestionsPerPage); // Display the previous set of questions
            }
        }
    }
}