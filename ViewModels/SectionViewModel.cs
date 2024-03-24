using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace stress_check_avalonia
{
    public class SectionViewModel : ReactiveObject
    {
        private static SectionViewModel _instance;

        private Section _currentSection;
        private Question _currentQuestion;
        private int _questionIndex;

        public List<QuestionViewModel> QuestionViewModels { get; private set; }

        public SectionViewModel()
        {
            CurrentSection = LoadSections.sections[0];
            QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q)).ToList();
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
            set { this.RaiseAndSetIfChanged(ref _currentSection, value); }
        }

        public void SetCurrentSection(int newSectionIndex)
        {
            if (newSectionIndex >= 0 && newSectionIndex < LoadSections.sections.Count)
            {
                CurrentSection = LoadSections.sections[newSectionIndex];
                QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q)).ToList();
            }
        }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                if (_currentQuestion != value)
                {
                    _currentQuestion = value;
                }
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

        public void HandleChoiceSelect(string choice, string groupName)
        {
            // Convert the choice to an integer
            var choiceValue = int.TryParse(choice, out int result) ? result : 0;

            // Convert the GroupName to an integer and use it to set the CurrentQuestion
            if (int.TryParse(groupName, out int questionIndex) && questionIndex < Questions.Count)
            {
                CurrentQuestion = Questions[questionIndex];
            }

            if (CurrentQuestion != null)
            {
                // Update the score of the current question
                CurrentQuestion.Score = choiceValue;

                // Output the updated score to the console for debugging
                System.Diagnostics.Debug.WriteLine($"CurrentQuestion ID: {CurrentQuestion.Id}, Updated Score: {CurrentQuestion.Score}");

                // Calculate the score of the current section
                var sectionScore = ScoreCalculator.CalculateScore(Questions);
            }
            // Update the scores state
            // This part depends on how you manage the scores of the sections
        }
    }
}
