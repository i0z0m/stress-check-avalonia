using ReactiveUI;

namespace stress_check_avalonia
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

        public void HandleChoiceSelect(int choiceValue)
        {
            // Update the score of the question
            Question.Score = choiceValue;

            // Output the updated score to the console for debugging
            System.Diagnostics.Debug.WriteLine($"Question ID: {Question.Id}, Updated Score: {Question.Score}");
        }
    }
}