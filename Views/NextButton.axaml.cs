using StressCheckAvalonia.ViewModels;
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
            var mainWindow = this.FindAncestorOfType<MainWindow>();
            if (mainWindow != null)
            {
                if (mainWindow.FindControl<ContentControl>("EmployeeInformationControl").IsVisible)
                {
                    // Set IsSectionActive to true when moving to the questions
                    SectionViewModel.Instance.IsSectionActive = true;

                    // Set IsInput to true
                    SectionViewModel.Instance.IsInput = true;

                    if (mainWindow.IsEmployeeInformationComplete())
                    {
                        // Set IsInput to false
                        SectionViewModel.Instance.IsInput = false;

                        // Hide EmployeeInformationControl and show QuestionsPanel when NextButton is clicked
                        mainWindow.FindControl<ContentControl>("EmployeeInformationControl").IsVisible = false;
                        mainWindow.FindControl<StackPanel>("QuestionsPanel").IsVisible = true;

                        // Display the first set of questions
                        mainWindow.DisplayQuestions(0, mainWindow.QuestionsPerPage);
                    }
                    else
                    {
                        // Handle incomplete EmployeeInformation here
                        // Validate the input fields in the EmployeeViewModel
                        EmployeeViewModel.Instance.ValidateInput();
                    }
                }
                else if (mainWindow.AreAllQuestionsAnswered())
                {
                    // Set IsInput to false when you navigate away from the EmployeeInformationControl
                    SectionViewModel.Instance.IsInput = false;

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
                                // Set IsSectionActive to true when moving to the next section
                                SectionViewModel.Instance.IsSectionActive = true;

                                // Increment the section index
                                currentIndex++;

                                // Reset the question start index
                                mainWindow.QuestionStartIndex = 0;
                            }
                            else // If it's the last section
                            {
                                // Set IsSectionActive to false when showing the results
                                SectionViewModel.Instance.IsSectionActive = false;

                                // Hide QuestionsPanel and show ResultsContent
                                mainWindow.FindControl<StackPanel>("QuestionsPanel").IsVisible = false;
                                mainWindow.FindControl<ContentControl>("ResultsContent").IsVisible = true;

                                // Update the AppTitle
                                SectionViewModel.Instance.IsAggregated = true;

                                // Show the results
                                mainWindow.ShowResults();
                            }
                        }
                        else
                        {
                            // Update the question start index
                            mainWindow.QuestionStartIndex += mainWindow.QuestionsPerPage;
                        }

                        // Load new section
                        mainWindow.DisplayQuestions(currentIndex, mainWindow.QuestionsPerPage); // Display the next set of questions
                        System.Diagnostics.Debug.WriteLine($"Loading section at index {currentIndex}");
                    }
                }
            }
        }
    }