using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Views;

namespace StressCheckAvalonia.Views
{
    public partial class BackButtons : UserControl
    {
        public BackButtons()
        {
            InitializeComponent();
            DataContext = StateViewModel.Instance;
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            if (sender is Button button && this.FindAncestorOfType<MainWindow>() is MainWindow mainWindow)
            {
                if (button.Name == "BackToTitleButton")
                {
                    StateViewModel.Instance.CurrentState = State.Input;
                    SectionViewModel.Instance.SetCurrentSection(0);
                    SectionViewModel.Instance.QuestionStartIndex = 0;
                }
                else if (button.Name == "BackOneScreenButton")
                {
                    if (StateViewModel.Instance.IsSectionActive)
                    {
                        int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);

                        if (SectionViewModel.Instance.QuestionStartIndex == 0)
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
                                SectionViewModel.Instance.QuestionStartIndex = (previousSectionQuestionCount - 1) / SectionViewModel.Instance.QuestionsPerPage * SectionViewModel.Instance.QuestionsPerPage;

                                // Load previous section or page
                                mainWindow.DisplayQuestions(currentIndex, SectionViewModel.Instance.QuestionsPerPage); // Display the previous set of questions

                                // Set the current state to SectionActive after the new section is loaded
                                StateViewModel.Instance.CurrentState = State.SectionActive;
                            }
                            else
                            {
                                StateViewModel.Instance.CurrentState = State.Input;
                            }
                        }
                        else
                        {
                            // Update the question start index
                            SectionViewModel.Instance.QuestionStartIndex -= SectionViewModel.Instance.QuestionsPerPage;

                            // Load previous section or page
                            mainWindow.DisplayQuestions(currentIndex, SectionViewModel.Instance.QuestionsPerPage); // Display the previous set of questions
                        }
                    }
                    else if (mainWindow.AggregateResultsControl != null && StateViewModel.Instance.IsAggregated)
                    {
                        // Display the last page of the last section
                        int lastSectionIndex = LoadSections.sections.Count - 1;
                        mainWindow.DisplayQuestions(lastSectionIndex, SectionViewModel.Instance.QuestionsPerPage);
                        SectionViewModel.Instance.QuestionStartIndex = (LoadSections.sections[lastSectionIndex].Questions.Count - 1) / SectionViewModel.Instance.QuestionsPerPage * SectionViewModel.Instance.QuestionsPerPage;

                        // Set the current state to SectionActive after the new section is loaded
                        StateViewModel.Instance.CurrentState = State.SectionActive;
                    }
                }
            }
        }
    }
}