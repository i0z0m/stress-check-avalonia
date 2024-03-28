using System.Collections.Generic;
using System.Linq;

namespace stress_check_avalonia
{
    public static class ValueCalculator
    {
        private static int CalculateSubtractionPattern(List<Question> questions)
        {
            int totalScore = CalculateAdditionPattern(questions);
            return (questions.Count * 5) - totalScore;
        }

        private static int CalculateAdditionPattern(List<Question> questions)
        {
            return questions.Sum(question => question.Score);
        }

        private static int CalculateComplexPattern(List<Question> questions)
        {
            int subtractionTotal = CalculateSubtractionPattern(questions.GetRange(0, questions.Count - 1));
            int additionScore = CalculateAdditionPattern(questions.GetRange(questions.Count - 1, 1));

            return subtractionTotal + additionScore;
        }

        public static int CalculateValue(this List<Question> questions, Factor factor)
        {
            List<Question> filteredQuestions = questions.Where(question => factor.Items?.Contains(question.Id) ?? false).ToList();
            int score = 0;

            score = factor.Type switch
            {
                "subtraction" => CalculateSubtractionPattern(filteredQuestions),
                "addition" => CalculateAdditionPattern(filteredQuestions),
                "complex" => CalculateComplexPattern(filteredQuestions),
                _ => 0
            };

            Rate rate = factor.Rates?.FirstOrDefault(rate => score >= rate.Min && score <= rate.Max);
            return rate != null ? rate.Value : 0;
        }
    }
}