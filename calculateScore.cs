using System.Collections.Generic;
using System.Linq;

public static class ScoreCalculator
{
    public static int CalculateScore(List<Question> questions)
    {
        return questions.Sum(question => question.Reverse ? 5 - question.Score : question.Score);
    }
}