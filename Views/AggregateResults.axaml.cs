using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace StressCheckAvalonia
{
    public partial class AggregateResults : UserControl
    {
        public Employee? Employee { get; private set; } // Make Employee nullable
        public List<Section> Sections { get; }

        public AggregateResults()
        {
            InitializeComponent();
            DataContext = this;
            Sections = new List<Section>(); // Initialize Sections
        }

        public void DisplayResults(Employee employee)
        {
            this.Employee = EmployeeViewModel.Instance.Employee;

            var sectionPanel = this.FindControl<StackPanel>("SectionPanel");

            // Clear the sectionPanel before adding new elements
            sectionPanel.Children.Clear();

            // Create TextBlock for Employee Level
            var scores = LoadSections.sections.Select(s => s.Scores).ToList();
            var values = LoadSections.sections.Select(s => new List<int> { s.Values }).ToList();
            var levelResult = LevelCalculator.CalculateLevel(scores, values);
            Employee.Level = levelResult.Method1 && levelResult.Method2 ? "High" : "Low";
            System.Diagnostics.Debug.WriteLine($"Employee.Level is set to {Employee.Level}");

            var employeeLevelTextBlock = new TextBlock
            {
                Text = Employee.Level == "High" ? "高ストレス者です" : "低ストレス者です",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            sectionPanel.Children.Add(employeeLevelTextBlock);

            // Create Grid for each Section
            var grid = new Grid();
            for (int i = 0; i < LoadSections.sections.Count - 1; i++) // Exclude the 4th section
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));

            int columnIndex = 0;
            foreach (var section in LoadSections.sections.Take(LoadSections.sections.Count - 1)) // Exclude the 4th section
            {
                var sectionNameTextBlock = new TextBlock
                {
                    Text = $"STEP {section.Step} {section.Name}",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                };
                Grid.SetColumn(sectionNameTextBlock, columnIndex);
                Grid.SetRow(sectionNameTextBlock, 0);
                grid.Children.Add(sectionNameTextBlock);

                var sectionScoreTextBlock = new TextBlock
                {
                    Text = $"スコアの合計: {section.Scores}",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                };
                Grid.SetColumn(sectionScoreTextBlock, columnIndex);
                Grid.SetRow(sectionScoreTextBlock, 1);
                grid.Children.Add(sectionScoreTextBlock);

                var sectionTotalTextBlock = new TextBlock
                {
                    Text = $"評価点の合計: {section.Values}",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                };
                Grid.SetColumn(sectionTotalTextBlock, columnIndex);
                Grid.SetRow(sectionTotalTextBlock, 2);
                grid.Children.Add(sectionTotalTextBlock);

                // Add RadarChart for this section here
                // var radarChart = new RadarChart();
                // if (section.Factors != null)
                // {
                //     radarChart.Items = section.Factors.Select((factor, index) => new RadarChartData
                //     {
                //         Index = index,
                //         Value = factor.Value
                //     }).ToList();
                // }
                var radarChart = new RadarChart();
                radarChart.Items = new List<RadarChartData>
                {
                    new RadarChartData { Index = 0, Value = 3 },
                    new RadarChartData { Index = 1, Value = 4 },
                    new RadarChartData { Index = 2, Value = 2 },
                    new RadarChartData { Index = 3, Value = 5 },
                    new RadarChartData { Index = 4, Value = 1 },
                    new RadarChartData { Index = 5, Value = 3 }
                };
                radarChart.Width = 400; // RadarChartの幅を2倍に
                radarChart.Height = 400; // RadarChartの高さを2倍に
                radarChart.Margin = new Thickness(10); // 必要に応じてマージンを調整

                Grid.SetColumn(radarChart, columnIndex);
                Grid.SetRow(radarChart, 3); // RadarChartを配置する行を指定
                grid.Children.Add(radarChart);

                columnIndex++;
            }

            sectionPanel.Children.Add(grid);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}