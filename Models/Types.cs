using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StressCheckAvalonia.Models
{
    public enum State
    {
        Input,
        SectionActive,
        Aggregated
    }

    public class Employee
    {
        public string Gender { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Furigana { get; set; } = string.Empty;
        public DateTimeOffset Birthday { get; set; }
        public string ID { get; set; } = string.Empty;
        public string Workplace { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Extension { get; set; }
    }

    public class Section
    {
        public int Step { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ReadOnlyCollection<Question>? Questions { get; }
        public int Scores { get; set; }
        public ReadOnlyCollection<string>? Choices { get; }
        public string? Group { get; set; }
        public ReadOnlyCollection<Factor>? Factors { get; }
        public int Values { get; set; }

        public Section(IEnumerable<Question>? questions, IEnumerable<string>? choices, IEnumerable<Factor>? factors)
        {
            Questions = questions?.ToList().AsReadOnly();
            Choices = choices?.ToList().AsReadOnly();
            Factors = factors?.ToList().AsReadOnly();
        }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Score { get; set; }
        public bool Reverse { get; set; }
    }

    public class Factor
    {
        public int Point { get; set; }
        public string Scale { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Type { get; set; } = string.Empty;
        public ReadOnlyCollection<Rate>? Rates { get; }
        public ReadOnlyCollection<int>? Items { get; }

        public Factor(IEnumerable<Rate>? rates, IEnumerable<int>? items)
        {
            Rates = rates?.ToList().AsReadOnly();
            Items = items?.ToList().AsReadOnly();
        }
    }

    public class Rate
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }
    }
}