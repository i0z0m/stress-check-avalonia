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
            DataContext = SectionViewModel.Instance;
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();
            if (mainWindow != null)
            {
                var button = sender as Button;
                if (button != null)
                {
                    if (button.Name == "BackToTitleButton")
                    {
                        // Set IsSectionActive to false when moving back to the title
                        SectionViewModel.Instance.IsSectionActive = false;
                        // Set IsAggregated to false
                        SectionViewModel.Instance.IsAggregated = false;

                        SectionViewModel.Instance.SetCurrentSection(0);
                        SectionViewModel.Instance.QuestionStartIndex = 0;

                        // If 'Back to Title' button is clicked, show EmployeeInformationControl
                        mainWindow.FindControl<StackPanel>("QuestionsPanel").IsVisible = false;
                        mainWindow.FindControl<ContentControl>("EmployeeInformationControl").IsVisible = true;
                        if (mainWindow.AggregateResultsControl != null)
                        {
                            mainWindow.AggregateResultsControl.IsVisible = false;
                        }
                        // Set IsInput to true
                        SectionViewModel.Instance.IsInput = true;
                    }
                    else if (button.Name == "BackOneScreenButton")
                    {
                        if (mainWindow.FindControl<StackPanel>("QuestionsPanel").IsVisible)
                        {
                            int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);

                            if (SectionViewModel.Instance.QuestionStartIndex == 0)
                            {
                                if (currentIndex > 0) // Check if it's not the first section
                                {
                                    // Set IsSectionActive to true when moving back to the previous section
                                    SectionViewModel.Instance.IsSectionActive = true;

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
                                }
                                else
                                {
                                    // Set IsSectionActive to false when moving back to the title
                                    SectionViewModel.Instance.IsSectionActive = false;

                                    // If it's the first section, show EmployeeInformationControl
                                    mainWindow.FindControl<StackPanel>("QuestionsPanel").IsVisible = false;
                                    mainWindow.FindControl<ContentControl>("EmployeeInformationControl").IsVisible = true;
                                    // Set IsInput to true
                                    SectionViewModel.Instance.IsInput = true;
                                }
                            }
                            else
                            {
                                // Update the question start index
                                SectionViewModel.Instance.QuestionStartIndex -= SectionViewModel.Instance.QuestionsPerPage;
                            }

                            // Load previous section or page
                            mainWindow.DisplayQuestions(currentIndex, SectionViewModel.Instance.QuestionsPerPage); // Display the previous set of questions
                        }
                        else if (mainWindow.AggregateResultsControl != null && mainWindow.AggregateResultsControl.IsVisible)
                        {
                            SectionViewModel.Instance.IsSectionActive = true;

                            // Display the last page of the last section
                            int lastSectionIndex = LoadSections.sections.Count - 1;
                            mainWindow.DisplayQuestions(lastSectionIndex, SectionViewModel.Instance.QuestionsPerPage);
                            SectionViewModel.Instance.QuestionStartIndex = (LoadSections.sections[lastSectionIndex].Questions.Count - 1) / SectionViewModel.Instance.QuestionsPerPage * SectionViewModel.Instance.QuestionsPerPage;

                            // Make sure the QuestionsPanel is visible
                            mainWindow.QuestionsPanel.IsVisible = true;

                            // Make sure the SectionDescription is visible
                            mainWindow.SectionDescriptionControl.IsVisible = true;

                            // Hide the AggregateResults
                            mainWindow.AggregateResultsControl.IsVisible = false;

                            // Set IsAggregated to false
                            SectionViewModel.Instance.IsAggregated = false;
                        }
                    }
                }
            }
        }
    }
}