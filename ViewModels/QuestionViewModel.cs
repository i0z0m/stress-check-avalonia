using StressCheckAvalonia.Models;
using ReactiveUI;
using System.Collections.Generic;
using Avalonia.Media;
using System.Linq;

namespace StressCheckAvalonia.ViewModels
{
    public class QuestionViewModel : ReactiveObject
    {
        private readonly SectionViewModel _sectionViewModel;
        private Question _question;
        private IBrush _background;

        public QuestionViewModel(Question question, SectionViewModel sectionViewModel)
        {
            _question = question;
            _sectionViewModel = sectionViewModel;
            _background = Brushes.White;
        }

        public List<string> Choices => _sectionViewModel.Choices?.ToList() ?? new List<string>();

        public Question Question
        {
            get => _question;
            set => this.RaiseAndSetIfChanged(ref _question, value);
        }

        private bool _isAnswered;
        public bool IsAnswered
        {
            get { return _isAnswered; }
            set { this.RaiseAndSetIfChanged(ref _isAnswered, value); }
        }

        public IBrush Background
        {
            get => _background;
            set => this.RaiseAndSetIfChanged(ref _background, value);
        }

        public void HandleChoiceSelect(int choiceValue)
        {
            // Update the score of the question
            Question.Score = choiceValue;
            IsAnswered = true; // Set IsAnswered to true when a choice is selected

            // Set the background color to white
            Background = Brushes.White;

            // Output the updated score to the console for debugging
            System.Diagnostics.Debug.WriteLine($"Question ID: {Question.Id}, Updated Score: {Question.Score}");
        }

        public void ValidateAnswered()
        {
            if (!IsAnswered)
            {
                Background = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
            }
        }
    }
}