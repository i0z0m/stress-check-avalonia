using System;
using ReactiveUI;

namespace stress_check_avalonia
{
    public class QuestionViewModel : ReactiveObject
    {
        public QuestionViewModel(Question question)
        {
            Question = question;
            this.WhenAnyValue(x => x.SelectedChoice).Subscribe(choice => Question.Choice = choice);
        }

        private Question _question;

        public Question Question
        {
            get => _question;
            set => this.RaiseAndSetIfChanged(ref _question, value);
        }

        private int _selectedChoice;
        public int SelectedChoice
        {
            get => _selectedChoice;
            set => this.RaiseAndSetIfChanged(ref _selectedChoice, value);
        }
    }
}