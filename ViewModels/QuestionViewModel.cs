﻿using StressCheckAvalonia.Models;
using ReactiveUI;

namespace StressCheckAvalonia.ViewModels
{
    public class QuestionViewModel : ReactiveObject
    {
        public QuestionViewModel(Question question)
        {
            Question = question;
        }

        private Question _question;

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

        public void HandleChoiceSelect(int choiceValue)
        {
            // Update the score of the question
            Question.Score = choiceValue;
            IsAnswered = true; // Set IsAnswered to true when a choice is selected

            // Output the updated score to the console for debugging
            System.Diagnostics.Debug.WriteLine($"Question ID: {Question.Id}, Updated Score: {Question.Score}");
        }
    }
}