using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StressCheckAvalonia.ViewModels
{
    public class SectionViewModel : ReactiveObject
    {
        private static SectionViewModel? _instance;

        private Section? _currentSection;
        private int _questionIndex;
        private ReadOnlyCollection<QuestionViewModel> _questionViewModels;
        private ReadOnlyCollection<QuestionViewModel> _displayedQuestionViewModels;

        public SectionViewModel()
        {
            CurrentSection = LoadSections.Sections[0];
            _questionViewModels = new ReadOnlyCollection<QuestionViewModel>(CurrentSection?.Questions?.Select(q => new QuestionViewModel(q, this)).ToList() ?? []);
            _displayedQuestionViewModels = new ReadOnlyCollection<QuestionViewModel>([]);
        }

        public static SectionViewModel Instance
        {
            get
            {
                _instance ??= new SectionViewModel();
                return _instance;
            }
        }

        public Section? CurrentSection
        {
            get { return _currentSection; }
            set
            {
                this.RaiseAndSetIfChanged(ref _currentSection, value);
            }
        }

        public void SetCurrentSection(int newSectionIndex)
        {
            if (newSectionIndex >= 0 && newSectionIndex < LoadSections.Sections.Count)
            {
                CurrentSection = LoadSections.Sections[newSectionIndex];
                _questionViewModels = new ReadOnlyCollection<QuestionViewModel>(CurrentSection?.Questions?.Select(q => new QuestionViewModel(q, this)).ToList() ?? []);
            }
        }

        public int QuestionIndex
        {
            get { return _questionIndex; }
            set
            {
                if (_questionIndex != value)
                {
                    _questionIndex = value;
                }
            }
        }

        public ReadOnlyCollection<Question>? Questions => CurrentSection?.Questions;
        public ReadOnlyCollection<string>? Choices => CurrentSection?.Choices;

        public void UpdateScores()
        {
            if (CurrentSection != null && Questions != null)
            {
                CurrentSection.Scores = Questions.ToList().CalculateScore();
            }
        }

        public void UpdateValues()
        {
            if (CurrentSection?.Factors != null)
            {
                foreach (var factor in CurrentSection.Factors)
                {
                    factor.Value = Questions?.ToList().CalculateValue(factor) ?? 0;
                }

                CurrentSection.Values = CurrentSection.Factors.Sum(factor => factor.Value);
            }
        }

        public int QuestionStartIndex { get; set; }
        public int QuestionsPerPage { get; } = 10;

        public void UpdateDisplayedQuestions(int sectionIndex)
        {
            // Set the current section
            SetCurrentSection(sectionIndex);

            // Clear the existing questions
            _displayedQuestionViewModels = new ReadOnlyCollection<QuestionViewModel>([]);

            // Add each question to the DisplayedQuestionViewModels list
            for (int i = QuestionStartIndex; i < QuestionStartIndex + QuestionsPerPage && i < _questionViewModels.Count; i++)
            {
                var questionViewModel = _questionViewModels[i];

                // Add the question to the DisplayedQuestionViewModels list
                _displayedQuestionViewModels = new ReadOnlyCollection<QuestionViewModel>(_displayedQuestionViewModels.Concat([questionViewModel]).ToList());
            }
        }

        public bool AreAllQuestionsDisplayed()
        {
            var questions = Questions;
            return QuestionStartIndex + QuestionsPerPage >= questions?.Count;
        }

        public ReadOnlyCollection<QuestionViewModel> DisplayedQuestionViewModels => _displayedQuestionViewModels;

        public bool AreAllDisplayedQuestionsAnswered()
        {
            // Check if all currently displayed questions are answered
            return DisplayedQuestionViewModels.All(q => q.IsAnswered);
        }
    }
}