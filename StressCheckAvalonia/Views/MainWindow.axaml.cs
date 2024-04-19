using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia;

namespace StressCheckAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = StateViewModel.Instance;

            // Set the MainWindow instance in StateViewModel
            var stateViewModel = StateViewModel.Instance;
            if (stateViewModel != null)
            {
                stateViewModel.MainWindow = this;
                stateViewModel.HandleInputState();
            }
        }

        public void DisplayQuestions(int sectionIndex, int questionCount)
        {
            var sectionViewModel = SectionViewModel.Instance;
            if (sectionViewModel != null)
            {
                // Update the displayed questions in the SectionViewModel
                sectionViewModel.UpdateDisplayedQuestions(sectionIndex);

                // Clear the existing questions
                QuestionsPanel.Children.Clear();

                // Add each question and its corresponding choice buttons to the QuestionsPanel
                foreach (var questionViewModel in sectionViewModel.DisplayedQuestionViewModels)
                {
                    var questionText = new QuestionText
                    {
                        DataContext = questionViewModel, // Set the DataContext to the QuestionViewModel
                    };

                    var choiceButtons = new ChoiceButtons
                    {
                        DataContext = questionViewModel, // Set the DataContext to the QuestionViewModel
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                        Margin = new Thickness(0, 0, 30, 0)
                    };

                    // Set the IsChecked property of the RadioButton corresponding to the selected choice
                    var selectedChoice = questionViewModel.Question.Score;
                    if (selectedChoice >= 1 && selectedChoice <= 4)
                    {
                        var radioButton = choiceButtons.FindControl<RadioButton>($"RadioButton{selectedChoice}");
                        if (radioButton != null)
                        {
                            radioButton.IsChecked = true;
                            questionViewModel.HandleChoiceSelect(selectedChoice);
                        }
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
                        Margin = new Thickness(30, 0, 30, 10)
                    };
                    QuestionsPanel.Children.Add(underline);
                }
            }
        }
    }
}