using ReactiveUI;
using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.Views;
using System;
using System.Linq;

namespace StressCheckAvalonia.ViewModels
{
    public class StateViewModel : ReactiveObject
    {
        public MainWindow? MainWindow { get; set; }

        private static StateViewModel? _instance;
        private State _currentState;

        public static StateViewModel Instance
        {
            get
            {
                _instance ??= new StateViewModel();
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
            get => SectionViewModel.Instance.CurrentSection ?? new Section(null, null, null);
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
                    State.SectionActive => CurrentSection != null ? $"STEP {CurrentSection.Step} {CurrentSection.Name}" : "",
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
                    State.SectionActive => CurrentSection != null ? CurrentSection.Description : "",
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
                    State.SectionActive => "1つ後の画面へ進む",
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
                MainWindow?.DisplayQuestions(0, SectionViewModel.Instance.QuestionsPerPage);
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
            // Ensure the CurrentSection is not null before attempting to access it
            if (sectionViewModel.CurrentSection == null) return;

            int currentIndex = LoadSections.Sections.IndexOf(sectionViewModel.CurrentSection);

            if (isNext)
            {
                if (sectionViewModel.AreAllDisplayedQuestionsAnswered())
                {
                    sectionViewModel.UpdateScores();
                    sectionViewModel.UpdateValues();

                    if (currentIndex >= 0 && currentIndex < LoadSections.Sections.Count - 1 && sectionViewModel.AreAllQuestionsDisplayed())
                    {
                        currentIndex++;
                        sectionViewModel.QuestionStartIndex = 0;
                        MainWindow?.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage);
                        CurrentState = State.SectionActive;
                    }
                    else if (!sectionViewModel.AreAllQuestionsDisplayed())
                    {
                        sectionViewModel.QuestionStartIndex += sectionViewModel.QuestionsPerPage;
                        MainWindow?.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage);
                    }
                    else
                    {
                        HandleAggregatedState();
                    }
                }
                else
                {
                    foreach (var questionViewModel in sectionViewModel.DisplayedQuestionViewModels)
                    {
                        questionViewModel.ValidateAnswered();
                    }
                }
            }
            else
            {
                if (sectionViewModel.QuestionStartIndex == 0)
                {
                    if (currentIndex > 0)
                    {
                        sectionViewModel.UpdateScores();
                        sectionViewModel.UpdateValues();

                        currentIndex--;

                        var previousSection = LoadSections.Sections.ElementAtOrDefault(currentIndex);
                        if (previousSection?.Questions != null)
                        {
                            var previousSectionQuestionCount = previousSection.Questions.Count;
                            sectionViewModel.QuestionStartIndex = (previousSectionQuestionCount - 1) / sectionViewModel.QuestionsPerPage * sectionViewModel.QuestionsPerPage;
                        }

                        MainWindow?.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage);
                        CurrentState = State.SectionActive;
                    }
                    else
                    {
                        CurrentState = State.Input;
                    }
                }
                else
                {
                    sectionViewModel.QuestionStartIndex -= sectionViewModel.QuestionsPerPage;

                    var currentSection = LoadSections.Sections.ElementAtOrDefault(currentIndex);
                    if (currentSection?.Questions != null)
                    {
                        MainWindow?.DisplayQuestions(currentIndex, sectionViewModel.QuestionsPerPage);
                    }
                }
            }
        }

        public void HandleAggregatedState(bool isBackAction = false)
        {
            if (!isBackAction && SectionViewModel.Instance.AreAllQuestionsDisplayed())
            {
                CurrentState = State.Aggregated;
            }
            else
            {
                int lastSectionIndex = LoadSections.Sections.Count - 1;
                if (lastSectionIndex >= 0)
                {
                    var lastSection = LoadSections.Sections[lastSectionIndex];
                    if (lastSection?.Questions != null)
                    {
                        MainWindow?.DisplayQuestions(lastSectionIndex, SectionViewModel.Instance.QuestionsPerPage);
                        // Ensure lastSection.Questions is not null before accessing its properties
                        if (lastSection.Questions != null)
                        {
                            SectionViewModel.Instance.QuestionStartIndex = (lastSection.Questions.Count - 1) / SectionViewModel.Instance.QuestionsPerPage * SectionViewModel.Instance.QuestionsPerPage;
                        }
                    }
                }
                CurrentState = State.SectionActive;
            }
        }
    }
}
