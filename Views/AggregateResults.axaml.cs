using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Collections.Generic;
using System.Linq;

namespace StressCheckAvalonia.Views
{
    public partial class AggregateResults : UserControl
    {
        public List<Section> Sections { get; }

        public AggregateResults()
        {
            InitializeComponent();
            DataContext = EmployeeViewModel.Instance;
        }

        public void DisplayResults(Employee employee)
        {
            EmployeeViewModel.Instance.Employee = employee; // Update the EmployeeViewModel's Employee

            var sectionPanel = this.FindControl<StackPanel>("SectionPanel");

            // Clear the sectionPanel before adding new elements
            sectionPanel.Children.Clear();

            // Create TextBlock for Employee Level
            var scores = LoadSections.sections.Select(s => s.Scores).ToList();
            var values = LoadSections.sections.Select(s => new List<int> { s.Values }).ToList();
            var levelResult = LevelCalculator.CalculateLevel(scores, values);
            EmployeeViewModel.Instance.Employee.Level = levelResult.Method1 && levelResult.Method2 ? "High" : "Low";
            System.Diagnostics.Debug.WriteLine($"Employee.Level is set to {EmployeeViewModel.Instance.Employee.Level}");

            var employeeLevelTextBlock = new TextBlock
            {
                FontSize = 30,
                Text = EmployeeViewModel.Instance.Employee.Level == "High" ? "高ストレス者です" : "低ストレス者です",
                Foreground = new SolidColorBrush(EmployeeViewModel.Instance.Employee.Level == "High" ? Color.FromArgb(128, 255, 0, 0) : Color.FromArgb(128, 0, 0, 255)),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
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
                    Items = section.Factors.Select(factor => new RadarChartData
                    {
                        Label = factor.Scale,
                        Value = factor.Value,
                        Color = EmployeeViewModel.Instance.Employee.Level == "High" ? Color.FromArgb(128, 255, 0, 0) : Color.FromArgb(128, 0, 0, 255)
                    }).ToList(),

                    Width = 300, // Adjust size as needed
                    Height = 300,
                    Margin = new Thickness(20)
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