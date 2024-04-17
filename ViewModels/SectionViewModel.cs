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

        public ReadOnlyCollection<QuestionViewModel> QuestionViewModels { get; private set; }

        public SectionViewModel()
        {
            CurrentSection = LoadSections.Sections[0];
            QuestionViewModels = new ReadOnlyCollection<QuestionViewModel>(CurrentSection?.Questions?.Select(q => new QuestionViewModel(q, this)).ToList() ?? new List<QuestionViewModel>());
            DisplayedQuestionViewModels = new ReadOnlyCollection<QuestionViewModel>(new List<QuestionViewModel>());
        }

        public static SectionViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SectionViewModel();
                }
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
                QuestionViewModels = new ReadOnlyCollection<QuestionViewModel>(CurrentSection?.Questions?.Select(q => new QuestionViewModel(q, this)).ToList() ?? new List<QuestionViewModel>());
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
            CurrentSection.Scores = Questions.ToList().CalculateScore();
        }

        public void UpdateValues()
        {
            if (CurrentSection.Factors != null)
            {
                foreach (var factor in CurrentSection.Factors)
                {
                    factor.Value = Questions.ToList().CalculateValue(factor);
                }

                CurrentSection.Values = CurrentSection.Factors.Sum(factor => factor.Value);
            }
        }

        public int QuestionStartIndex { get; set; }
        public int QuestionsPerPage { get; } = 10;

        public void UpdateDisplayedQuestions(int sectionIndex, int questionCount)
        {
            // Set the current section
            SetCurrentSection(sectionIndex);

            // Clear the existing questions
            DisplayedQuestionViewModels = new ReadOnlyCollection<QuestionViewModel>(new List<QuestionViewModel>());

            // Add each question to the DisplayedQuestionViewModels list
            for (int i = QuestionStartIndex; i < QuestionStartIndex + QuestionsPerPage && i < this.QuestionViewModels.Count; i++)
            {
                var questionViewModel = this.QuestionViewModels[i];

                // Add the question to the DisplayedQuestionViewModels list
                DisplayedQuestionViewModels = new ReadOnlyCollection<QuestionViewModel>(DisplayedQuestionViewModels.Concat(new[] { questionViewModel }).ToList());
            }
        }

        public bool AreAllQuestionsDisplayed()
        {
            var questions = this.Questions;
            return QuestionStartIndex + QuestionsPerPage >= questions?.Count;
        }

        // Add a new property to hold the currently displayed questions
        public ReadOnlyCollection<QuestionViewModel> DisplayedQuestionViewModels { get; private set; }

        public bool AreAllDisplayedQuestionsAnswered()
        {
            // Check if all currently displayed questions are answered
            return DisplayedQuestionViewModels.All(q => q.IsAnswered);
        }
    }
}
