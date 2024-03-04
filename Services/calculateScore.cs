using System.Collections.Generic;
using System.Linq;

namespace stress_check_avalonia
{
    public static class ScoreCalculator
    {
        public static int CalculateScore(List<Question> questions)
        {
            return questions.Sum(question => question.Reverse ? 5 - question.Score : question.Score);
        }
    }
} 