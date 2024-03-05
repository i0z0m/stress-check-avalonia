using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace stress_check_avalonia
{
    public class SectionViewModel : INotifyPropertyChanged
    {
        private static SectionViewModel _instance;

        private Section _currentSection;
        private Question _currentQuestion;

        private SectionViewModel()
        {
            CurrentSection = LoadSections.sections[0];
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
                if (_currentSection != value)
                {
                    _currentSection = value;
                    OnPropertyChanged(nameof(CurrentSection));
                    OnPropertyChanged(nameof(Questions));
                }
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
                    OnPropertyChanged(nameof(CurrentQuestion));
                }
            }
        }

        public Question DisplayedQuestion { get; set; }

        public List<Question> Questions => CurrentSection.Questions;
        public List<string> Choices => CurrentSection.Choices;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void HandleChoiceSelect(string choice)
        {
            var choiceIndex = Choices.IndexOf(choice);
            var choiceValue = choiceIndex >= 0 ? choiceIndex + 1 : 0; // Add 1 to the index to start the score at 1

            CurrentQuestion = DisplayedQuestion;
            System.Diagnostics.Debug.WriteLine($"CurrentQuestion is: {CurrentQuestion}");

            if (CurrentQuestion != null)
            {
                // Update the score of the current question
                CurrentQuestion.Score = choiceValue;

                // Output the updated score to the console for debugging
                System.Diagnostics.Debug.WriteLine($"Question ID: {CurrentQuestion.Id}, Updated Score: {CurrentQuestion.Score}");

                // Calculate the score of the current section
                var sectionScore = ScoreCalculator.CalculateScore(Questions);
            }
            // Update the scores state
            // This part depends on how you manage the scores of the sections
        }
    }
}
