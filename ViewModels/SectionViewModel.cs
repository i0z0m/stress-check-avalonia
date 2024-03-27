using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace stress_check_avalonia
{
    public class SectionViewModel : ReactiveObject
    {
        private static SectionViewModel _instance;

        private Section _currentSection;
        private int _questionIndex;

        public List<QuestionViewModel> QuestionViewModels { get; private set; }

        public SectionViewModel()
        {
            CurrentSection = LoadSections.sections[0];
            QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q)).ToList();
        }

        public static SectionViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SectionViewModel();
                }
                return _instance;
            }
        }

        public Section CurrentSection
        {
            get { return _currentSection; }
            set { this.RaiseAndSetIfChanged(ref _currentSection, value); }
        }

        public void SetCurrentSection(int newSectionIndex)
        {
            if (newSectionIndex >= 0 && newSectionIndex < LoadSections.sections.Count)
            {
                CurrentSection = LoadSections.sections[newSectionIndex];
                QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q)).ToList();
            }
        }

        public int QuestionIndex
        {
            get { return _questionIndex; }
            set
            {
                if (_questionIndex != value)
                {
                    _questionIndex = value;
                }
            }
        }

        public List<Question> Questions => CurrentSection.Questions;
        public List<string> Choices => CurrentSection.Choices;
    }
}
