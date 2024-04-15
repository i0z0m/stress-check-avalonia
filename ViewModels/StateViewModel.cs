using ReactiveUI;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;
using StressCheckAvalonia.Views;
using System;
using System.Runtime.InteropServices;

namespace StressCheckAvalonia.ViewModels
{
    public class StateViewModel : ReactiveObject
    {
        public MainWindow MainWindow { get; set; }

        private static StateViewModel _instance;
        private State _currentState;

        public static StateViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateViewModel();
                }
                return _instance;
            }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentState, value);
                this.RaisePropertyChanged(nameof(IsInput));
                this.RaisePropertyChanged(nameof(IsSectionActive));
                this.RaisePropertyChanged(nameof(IsAggregated));
                this.RaisePropertyChanged(nameof(IsInputInverted));
                this.RaisePropertyChanged(nameof(AppTitle));
                this.RaisePropertyChanged(nameof(DescriptionText));
                this.RaisePropertyChanged(nameof(NextButtonText));
            }
        }

        public bool IsInput
        {
            get => CurrentState == State.Input;
        }
        public bool IsInputInverted => !IsInput;

        public bool IsSectionActive
        {
            get => CurrentState == State.SectionActive;
        }

        public bool IsAggregated
        {
            get => CurrentState == State.Aggregated;
        }

        public Section CurrentSection
        {
            get => SectionViewModel.Instance.CurrentSection;
            set
            {
                SectionViewModel.Instance.CurrentSection = value;
                this.RaisePropertyChanged(nameof(AppTitle));
            }
        }

        public string AppTitle
        {
            get
            {
                return CurrentState switch
                {
                    State.Input => "ストレスチェック開始",
                    State.SectionActive => $"STEP {CurrentSection.Step} {CurrentSection.Name}",
                    State.Aggregated => "ストレスチェック終了",
                    _ => throw new InvalidOperationException("Undefined state for AppTitle"),
                };
            }
        }

        public string DescriptionText
        {
            get
            {
                return CurrentState switch
                {
                    State.Input => "必須事項を入力してください。",
                    State.SectionActive => CurrentSection.Description,
                    State.Aggregated => "これで質問は、終わりです。お疲れさまでした。",
                    _ => throw new InvalidOperationException("Undefined state for DescriptionText"),
                };
            }
        }

        public string NextButtonText
        {
            get
            {
                return CurrentState switch
                {
                    State.Input => "入力を完了して開始",
                    State.SectionActive => "1つ先の画面へ進む",
                    State.Aggregated => "結果を保存して終了",
                    _ => throw new InvalidOperationException("Undefined state for NextButtonText"),
                };
            }
        }
        public void HandleInputState(bool shouldValidateInput = false)
        {
            if (EmployeeViewModel.Instance.IsInformationComplete())
            {
                CurrentState = State.SectionActive;
                MainWindow.DisplayQuestions(0, SectionViewModel.Instance.QuestionsPerPage);
            }
            else if (shouldValidateInput)
            {
                // Highlight the incomplete fields
                EmployeeViewModel.Instance.ValidateInput();
            }
        }

        public void HandleSectionActiveState(bool isNext)
        {
            var sectionViewModel = SectionViewModel.Instance;
            int currentIndex = LoadSections.sections.IndexOf(sectionViewModel.CurrentSection);

            if (isNext)
            {
                if (sectionViewModel.AreAllDisplayedQuestionsAnswered()) // Check if all currently displayed questions are answered
                {
                    // Update the score and values of the current section
                    sectionViewModel.UpdateScores();
                    sectionViewModel.UpdateValues();

                    if (currentIndex < LoadSections.sections.Count - 1 && sectionViewModel.AreAllQuestionsDisplayed()) // Check if it's not the last section and all questions are displayed
                    {
                        // Increment the section index
                        currentIndex++;

                        // Reset the question start index
                        sectionViewModel.QuestionStartIndex = 0;

                        // Load new section
                        MainWindow.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage); // Display the next set of questions

                        // Set the current state to SectionActive after the new section is loaded
                        CurrentState = State.SectionActive;
                    }
                    else if (!sectionViewModel.AreAllQuestionsDisplayed()) // If not all questions are displayed
                    {
                        // Update the question start index
                        sectionViewModel.QuestionStartIndex += sectionViewModel.QuestionsPerPage;

                        // Load new section
                        MainWindow.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage); // Display the next set of questions
                    }
                    else // If it's the last section and all questions are displayed
                    {
                        // Call HandleAggregatedState to show the results
                        HandleAggregatedState();
                    }
                }
                else // If not all currently displayed questions are answered
                {
                    foreach (var questionViewModel in CollectionsMarshal.AsSpan(sectionViewModel.DisplayedQuestionViewModels))
                    {
                        questionViewModel.ValidateAnswered();
                    }
                }
            }
            else // if it's a back action
            {
                if (sectionViewModel.QuestionStartIndex == 0)
                {
                    if (currentIndex > 0) // Check if it's not the first section
                    {
                        // Update the score and values of the current section
                        sectionViewModel.UpdateScores();
                        sectionViewModel.UpdateValues();

                        // Decrement the section index
                        currentIndex--;

                        // Set the question start index to the first question of the last page of the previous section
                        var previousSectionQuestionCount = LoadSections.sections[currentIndex].Questions.Count;
                        sectionViewModel.QuestionStartIndex = (previousSectionQuestionCount - 1) / sectionViewModel.QuestionsPerPage * sectionViewModel.QuestionsPerPage;

                        // Load previous section or page
                        MainWindow.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage); // Display the previous set of questions

                        // Set the current state to SectionActive after the new section is loaded
                        CurrentState = State.SectionActive;
                    }
                    else
                    {
                        CurrentState = State.Input;
                    }
                }
                else
                {
                    // Update the question start index
                    sectionViewModel.QuestionStartIndex -= sectionViewModel.QuestionsPerPage;

                    // Load previous section or page
                    MainWindow.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage); // Display the previous set of questions
                }
            }
        }

        public void HandleAggregatedState(bool isBackAction = false)
        {
            if (!isBackAction && SectionViewModel.Instance.AreAllQuestionsDisplayed())
            {
                CurrentState = State.Aggregated;
                // Show the results
                MainWindow.ShowResults();
            }
            else
            {
                // Display the last page of the last section
                int lastSectionIndex = LoadSections.sections.Count - 1;
                MainWindow.DisplayQuestions(lastSectionIndex, SectionViewModel.Instance.QuestionsPerPage);
                SectionViewModel.Instance.QuestionStartIndex = (LoadSections.sections[lastSectionIndex].Questions.Count - 1) / SectionViewModel.Instance.QuestionsPerPage * SectionViewModel.Instance.QuestionsPerPage;

                // Set the current state to SectionActive after the new section is loaded
                CurrentState = State.SectionActive;
            }
        }
    }
}
