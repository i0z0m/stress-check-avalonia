using System;
using System.Collections.Generic;

namespace StressCheckAvalonia
{
    public class Employee
    {
        public string Gender { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string Furigana { get; set; }
        public DateTime Birthday { get; set; }
        public string ID { get; set; }
        public string Workplace { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Extension { get; set; }
    }

    public class Section
    {
        public int Step { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Next { get; set; }
        public List<Question>? Questions { get; set; }
        public int Scores { get; set; }
        public List<string>? Choices { get; set; }
        public string? Group { get; set; }
        public List<Factor>? Factors { get; set; }
        public int Values { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public bool Reverse { get; set; }
    }

    public class Factor
    {
        public int Point { get; set; }
        public string Scale { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
        public List<Rate>? Rates { get; set; }
        public List<int>? Items { get; set; }
    }

    public class Rate
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }
    }
}