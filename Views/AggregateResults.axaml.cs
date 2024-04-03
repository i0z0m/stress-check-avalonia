using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace StressCheckAvalonia.Views
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

                // Create a TextBlock for the section group
                var sectionGroupTextBlock = new TextBlock
                {
                    Text = section.Group,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                };
                Grid.SetColumn(sectionGroupTextBlock, columnIndex);
                Grid.SetRow(sectionGroupTextBlock, 3);
                grid.Children.Add(sectionGroupTextBlock);

                // Add RadarChart for this section here
                // Assume this part is inside the DisplayResults method or a similar context
                var radarChart = new RadarChart
                {
                    // Directly use the section's factors (or similar data) to populate the RadarChart
                    // Assuming each section has a collection of factors that we want to visualize
                    Items = section.Factors.Select(factor => new RadarChartData
                    {
                        Label = factor.Scale,
                        Value = factor.Value
                    }).ToList(),

                    Width = 400,
                    Height = 400,
                    Margin = new Thickness(10)
                };

                Grid.SetColumn(radarChart, columnIndex);
                Grid.SetRow(radarChart, 4);
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