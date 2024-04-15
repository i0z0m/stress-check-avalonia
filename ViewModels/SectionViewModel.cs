using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace StressCheckAvalonia.ViewModels
{
    public class SectionViewModel : ReactiveObject
    {
        private static SectionViewModel _instance;

        private Section _currentSection;
        private int _questionIndex;

        public List<QuestionViewModel> QuestionViewModels { get; private set; }

        public SectionViewModel()
        {
            CurrentSection = LoadSections.sections[0];
            QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q, this)).ToList();
            DisplayedQuestionViewModels = new List<QuestionViewModel>();
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

        public Section CurrentSection
        {
            get { return _currentSection; }
            set
            {
                this.RaiseAndSetIfChanged(ref _currentSection, value);
            }
        }

        public void SetCurrentSection(int newSectionIndex)
        {
            if (newSectionIndex >= 0 && newSectionIndex < LoadSections.sections.Count)
            {
                CurrentSection = LoadSections.sections[newSectionIndex];
                QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q, this)).ToList();
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

        public List<Question> Questions => CurrentSection.Questions;
        public List<string> Choices => CurrentSection.Choices;

        public void UpdateScores()
        {
            CurrentSection.Scores = Questions.CalculateScore();
        }

        public void UpdateValues()
        {
            if (CurrentSection.Factors != null)
            {
                foreach (var factor in CurrentSection.Factors)
                {
                    factor.Value = Questions.CalculateValue(factor);
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
            DisplayedQuestionViewModels.Clear();

            // Add each question to the DisplayedQuestionViewModels list
            for (int i = QuestionStartIndex; i < QuestionStartIndex + QuestionsPerPage && i < this.QuestionViewModels.Count; i++)
            {
                var questionViewModel = this.QuestionViewModels[i];

                // Add the question to the DisplayedQuestionViewModels list
                DisplayedQuestionViewModels.Add(questionViewModel);
            }
        }

        public bool AreAllQuestionsDisplayed()
        {
            var questions = this.Questions;
            return QuestionStartIndex + QuestionsPerPage >= questions.Count;
        }

        // Add a new property to hold the currently displayed questions
        public List<QuestionViewModel> DisplayedQuestionViewModels { get; set; }

        public bool AreAllDisplayedQuestionsAnswered()
        {
            // Check if all currently displayed questions are answered
            return DisplayedQuestionViewModels.All(q => q.IsAnswered);
        }
    }
}