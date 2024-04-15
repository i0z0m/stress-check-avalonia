using StressCheckAvalonia.ViewModels;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace StressCheckAvalonia.Views
{
    public partial class NextButton : UserControl
    {
        public NextButton()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            if (sender is Button && this.FindAncestorOfType<MainWindow>() is MainWindow mainWindow)
            {
                if (SectionViewModel.Instance.IsInput)
                {
                    SectionViewModel.Instance.CurrentState = State.SectionActive;

                    if (EmployeeViewModel.Instance.IsInformationComplete())
                    {
                        // Display the first set of questions
                        mainWindow.DisplayQuestions(0, SectionViewModel.Instance.QuestionsPerPage);
                    }
                    else
                    {
                        // Handle incomplete EmployeeInformation here
                        // Validate the input fields in the EmployeeViewModel
                        EmployeeViewModel.Instance.ValidateInput();
                    }
                }
                else
                {
                    var sectionViewModel = SectionViewModel.Instance;
                    if (!sectionViewModel.AreAllDisplayedQuestionsAnswered())
                    {
                        foreach (var questionViewModel in sectionViewModel.DisplayedQuestionViewModels)
                        {
                            questionViewModel.ValidateAnswered();
                        }
                    }
                    else
                    {
                        int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);

                        if (SectionViewModel.Instance.AreAllQuestionsDisplayed())
                        {
                            // Update the score and values of the current section
                            SectionViewModel.Instance.UpdateScores();
                            SectionViewModel.Instance.UpdateValues();

                            // Output the section score and values to the console for debugging
                            System.Diagnostics.Debug.WriteLine($"Section Score: {SectionViewModel.Instance.CurrentSection.Scores}");
                            System.Diagnostics.Debug.WriteLine($"Section Values: {SectionViewModel.Instance.CurrentSection.Values}");

                            if (currentIndex < LoadSections.sections.Count - 1) // Check if it's not the last section
                            {
                                SectionViewModel.Instance.CurrentState = State.SectionActive;

                                // Increment the section index
                                currentIndex++;

                                // Reset the question start index
                                SectionViewModel.Instance.QuestionStartIndex = 0;
                            }
                            else // If it's the last section
                            {
                                // Show the results
                                mainWindow.ShowResults();
                            }
                        }
                        else
                        {
                            // Update the question start index
                            SectionViewModel.Instance.QuestionStartIndex += SectionViewModel.Instance.QuestionsPerPage;
                        }

                        // Load new section
                        mainWindow.DisplayQuestions(currentIndex, SectionViewModel.Instance.QuestionsPerPage); // Display the next set of questions
                        System.Diagnostics.Debug.WriteLine($"Loading section at index {currentIndex}");
                    }
                }
            }
        }
    }
}