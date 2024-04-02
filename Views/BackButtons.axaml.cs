using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace StressCheckAvalonia.Views
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
                if (mainWindow.AggregateResultsControl != null && mainWindow.AggregateResultsControl.IsVisible)
                {
                    // Display the last page of the last section
                    int lastSectionIndex = LoadSections.sections.Count - 1;
                    mainWindow.DisplayQuestions(lastSectionIndex, mainWindow.QuestionsPerPage);
                    mainWindow.QuestionStartIndex = (LoadSections.sections[lastSectionIndex].Questions.Count - 1) / mainWindow.QuestionsPerPage * mainWindow.QuestionsPerPage;

                    // Make sure the QuestionsPanel is visible
                    mainWindow.QuestionsPanel.IsVisible = true;

                    //Make sure the SectionDescription is visible
                    mainWindow.SectionDescriptionControl.IsVisible = true;

                    // Hide the AggregateResults
                    mainWindow.AggregateResultsControl.IsVisible = false;
                }
                else
                {
                    int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);

                    if (mainWindow.QuestionStartIndex == 0)
                    {
                        if (currentIndex > 0) // Check if it's not the first section
                        {
                            // Update the score and values of the current section
                            SectionViewModel.Instance.UpdateScores();
                            SectionViewModel.Instance.UpdateValues();

                            // Output the section score and values to the console for debugging
                            System.Diagnostics.Debug.WriteLine($"Section Score: {SectionViewModel.Instance.CurrentSection.Scores}");
                            System.Diagnostics.Debug.WriteLine($"Section Values: {SectionViewModel.Instance.CurrentSection.Values}");

                            // Decrement the section index
                            currentIndex--;

                            // Set the question start index to the first question of the last page of the previous section
                            var previousSectionQuestionCount = LoadSections.sections[currentIndex].Questions.Count;
                            mainWindow.QuestionStartIndex = (previousSectionQuestionCount - 1) / mainWindow.QuestionsPerPage * mainWindow.QuestionsPerPage;
                        }
                    }
                    else
                    {
                        // Update the question start index
                        mainWindow.QuestionStartIndex -= mainWindow.QuestionsPerPage;
                    }

                    // Load previous section or page
                    mainWindow.DisplayQuestions(currentIndex, mainWindow.QuestionsPerPage); // Display the previous set of questions
                }
            }
        }
    }
}