using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using stress_check_avalonia;

namespace stress_check_avalonia
{
    public partial class NextButton : UserControl
    {
        public NextButton()
        {
            InitializeComponent();
        }

        // In NextButton.axaml.cs
        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();
            if (mainWindow != null && mainWindow.AreAllQuestionsAnswered())
            {
                int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);

                if (mainWindow.AreAllQuestionsDisplayed())
                {
                    // Update the score and values of the current section
                    SectionViewModel.Instance.UpdateScores();
                    SectionViewModel.Instance.UpdateValues();

                    // Output the section score and values to the console for debugging
                    System.Diagnostics.Debug.WriteLine($"Section Score: {SectionViewModel.Instance.CurrentSection.Scores}");
                    System.Diagnostics.Debug.WriteLine($"Section Values: {SectionViewModel.Instance.CurrentSection.Values}");

                    if (currentIndex < LoadSections.sections.Count - 1) // Check if it's not the last section
                    {
                        // Increment the section index
                        currentIndex++;

                        // Reset the question start index
                        mainWindow.QuestionStartIndex = 0;
                    }
                }
                else
                {
                    // Update the question start index
                    mainWindow.QuestionStartIndex += mainWindow.QuestionsPerPage;
                }

                // Load new section
                mainWindow.InitSections(currentIndex, mainWindow.QuestionsPerPage); // Display the next set of questions
            }
        }
    }
}
