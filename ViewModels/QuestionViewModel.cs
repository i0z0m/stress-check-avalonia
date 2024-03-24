using ReactiveUI;
using stress_check_avalonia;

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
}