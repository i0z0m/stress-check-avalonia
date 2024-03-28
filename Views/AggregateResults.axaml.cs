using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;

namespace stress_check_avalonia
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
                Text = Employee.Level == "High" ? "���X�g���X�҂ł�" : "��X�g���X�҂ł�",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
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
                    Text = $"�X�R�A�̍��v: {section.Scores}",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                };
                Grid.SetColumn(sectionScoreTextBlock, columnIndex);
                Grid.SetRow(sectionScoreTextBlock, 1);
                grid.Children.Add(sectionScoreTextBlock);

                var sectionTotalTextBlock = new TextBlock
                {
                    Text = $"�]���_�̍��v: {section.Values}",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                };
                Grid.SetColumn(sectionTotalTextBlock, columnIndex);
                Grid.SetRow(sectionTotalTextBlock, 2);
                grid.Children.Add(sectionTotalTextBlock);

                // Add RadarChart for this section here

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