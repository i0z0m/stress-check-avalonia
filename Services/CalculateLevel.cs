using System.Collections.Generic;
using System.Linq;

namespace StressCheckAvalonia
{
    public class LevelResult
    {
        public bool Method1 { get; set; }
        public bool Method2 { get; set; }
        public List<int> Totals { get; set; }
    }

    public static class LevelCalculator
    {
        public static LevelResult CalculateLevel(this List<int> scores, List<List<int>> values)
        {
            bool method1 = scores[2] >= 77 || (scores[1] + scores[3] >= 76 && scores[2] >= 63);

            List<int> totals = values.Select(value => value?.Sum() ?? 0).ToList();
            bool method2 = totals[2] <= 12 || (totals[1] + totals[3] <= 26 && totals[2] <= 17);

            return new LevelResult { Method1 = method1, Method2 = method2, Totals = totals };
        }
    }
}