using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using StressCheckAvalonia.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;

namespace StressCheckAvalonia.Views
{
    public partial class AggregateResults : UserControl
    {
        public AggregateResults()
        {
            InitializeComponent();
            DataContext = EmployeeViewModel.Instance;

            // Subscribe to changes in the CurrentState property
            var stateViewModel = StateViewModel.Instance;
            stateViewModel?.WhenAnyValue(x => x.CurrentState)
                .Subscribe(Observer.Create<State>(state =>
                {
                    if (state == State.Aggregated)
                    {
                        var employeeViewModel = EmployeeViewModel.Instance;
                        if (employeeViewModel?.Employee != null)
                        {
                            DisplayResults(employeeViewModel.Employee);
                        }
                        IsVisible = true;
                    }
                    else
                    {
                        IsVisible = false;
                    }
                }));
        }

        public void DisplayResults(Employee employee)
        {
            if (employee == null) return; // Add check to ensure employee is not null

            var employeeViewModel = EmployeeViewModel.Instance;
            if (employeeViewModel != null)
            {
                employeeViewModel.Employee = employee; // Safely update the EmployeeViewModel's Employee
            }

            var sectionPanel = this.FindControl<StackPanel>("SectionPanel");
            if (sectionPanel == null) return; // Ensure sectionPanel is not null

            // Clear the sectionPanel before adding new elements
            sectionPanel.Children.Clear();

            // Create TextBlock for Employee Level
            var scores = LoadSections.Sections.Select(s => s.Scores).ToList();
            var values = LoadSections.Sections.Select(s => s.Values).ToList();
            var levelResult = scores.CalculateLevel(values);

            if (employeeViewModel != null)
            {
                employeeViewModel.Level = levelResult.Method1 && levelResult.Method2 ? "High" : "Low";
            }

            // Create Grid for each Section
            var grid = new Grid();
            for (int i = 0; i < LoadSections.Sections.Count - 1; i++) // Exclude the 4th section
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));
            grid.RowDefinitions.Add(new RowDefinition(0, GridUnitType.Auto));

            int columnIndex = 0;
            foreach (var section in LoadSections.Sections.Take(LoadSections.Sections.Count - 1)) // Exclude the 4th section
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

                if (section.Factors != null)
                {
                    // Add RadarChart for this section here
                    var radarChart = new RadarChart
                    {
                        Items = section.Factors.Select(factor => new RadarChartData
                        {
                            Label = factor.Scale,
                            Value = factor.Value,
                            Color = employeeViewModel?.Level == "High" ? Color.FromArgb(128, 255, 0, 0) : Color.FromArgb(128, 0, 0, 255)
                        }).ToList(),

                        Width = 300, // Adjust size as needed
                        Height = 300,
                        Margin = new Thickness(20)
                    };

                    Grid.SetColumn(radarChart, columnIndex);
                    Grid.SetRow(radarChart, 4);
                    grid.Children.Add(radarChart);
                }

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