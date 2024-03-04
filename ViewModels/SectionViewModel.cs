using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace stress_check_avalonia
{
    public class SectionViewModel : INotifyPropertyChanged
    {
        private Section _section;
        private Question _currentQuestion;

        public Section Section
        {
            get { return _section; }
            set
            {
                if (_section != value)
                {
                    _section = value;
                    OnPropertyChanged(nameof(Section));
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

        public List<Question> Questions => Section.Questions;
        public List<string> Choices => Section.Choices;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void HandleChoiceSelect(string choice)
        {
            var choiceIndex = Choices.IndexOf(choice);
            var choiceValue = choiceIndex >= 0 ? choiceIndex + 1 : 0; // Add 1 to the index to start the score at 1

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
