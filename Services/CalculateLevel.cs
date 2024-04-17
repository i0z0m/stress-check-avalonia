using System.Collections.Generic;

namespace StressCheckAvalonia.Services
{
    public class LevelResult
    {
        public bool Method1 { get; set; }
        public bool Method2 { get; set; }
        public List<int> Totals { get; }

        public LevelResult(bool method1, bool method2, List<int> totals)
        {
            Method1 = method1;
            Method2 = method2;
            Totals = totals;
        }
    }

    public static class LevelCalculator
    {
        public static LevelResult CalculateLevel(this List<int> scores, List<int> values)
        {
            bool method1 = scores[1] >= 77 || (scores[0] + scores[2] >= 76 && scores[1] >= 63);
            bool method2 = values[1] <= 12 || (values[0] + values[2] <= 26 && values[1] <= 17);

            return new LevelResult(method1, method2, values);
        }
    }
}