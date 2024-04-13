using StressCheckAvalonia.Models;
using StressCheckAvalonia.Services;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using StressCheckAvalonia.ViewModels;

namespace StressCheckAvalonia.ViewModels
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
            QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q, this)).ToList();
            DisplayedQuestionViewModels = new List<QuestionViewModel>();
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
            set
            {
                this.RaiseAndSetIfChanged(ref _currentSection, value);
                this.RaisePropertyChanged(nameof(IsSectionActive));
                this.RaisePropertyChanged(nameof(IsSectionInactive));
                this.RaisePropertyChanged(nameof(DescriptionText));
            }
        }

        public void SetCurrentSection(int newSectionIndex)
        {
            if (newSectionIndex >= 0 && newSectionIndex < LoadSections.sections.Count)
            {
                CurrentSection = LoadSections.sections[newSectionIndex];
                QuestionViewModels = CurrentSection.Questions.Select(q => new QuestionViewModel(q, this)).ToList();
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
                    this.RaisePropertyChanged(nameof(IsSectionActive));
                    this.RaisePropertyChanged(nameof(IsSectionInactive));
                }
            }
        }

        public List<Question> Questions => CurrentSection.Questions;
        public List<string> Choices => CurrentSection.Choices;

        public void UpdateScores()
        {
            CurrentSection.Scores = Questions.CalculateScore();
        }

        public void UpdateValues()
        {
            if (CurrentSection.Factors != null)
            {
                foreach (var factor in CurrentSection.Factors)
                {
                    factor.Value = Questions.CalculateValue(factor);
                }

                CurrentSection.Values = CurrentSection.Factors.Sum(factor => factor.Value);
            }
        }

        private bool _isInput;
        public bool IsInput
        {
            get { return _isInput; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isInput, value);
                this.RaisePropertyChanged(nameof(IsInputInverted));
                this.RaisePropertyChanged(nameof(AppTitle));
                this.RaisePropertyChanged(nameof(DescriptionText));
            }
        }

        public bool IsInputInverted
        {
            get { return !IsInput; }
        }

        private bool _isAggregated;
        public bool IsAggregated
        {
            get { return _isAggregated; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isAggregated, value);
                this.RaisePropertyChanged(nameof(AppTitle));
                this.RaisePropertyChanged(nameof(DescriptionText));
                this.RaisePropertyChanged(nameof(NextButtonText));
            }
        }

        private string _appTitle;
        public string AppTitle
        {
            get
            {
                if (IsInput)
                {
                    return "�X�g���X�`�F�b�N�J�n";
                }
                else if (IsAggregated)
                {
                    return "�X�g���X�`�F�b�N�I��";
                }
                else
                {
                    return "�X�g���X�`�F�b�N���{��";
                }
            }
            set { this.RaiseAndSetIfChanged(ref _appTitle, value); }
        }

        private bool _isSectionActive;
        public bool IsSectionActive
        {
            get { return _isSectionActive && CurrentSection != null && QuestionIndex > 0; }
            set
            {
                this.RaiseAndSetIfChanged(ref _isSectionActive, value);
                IsSectionInactive = !value;
            }
        }

        private bool _isSectionInactive;
        public bool IsSectionInactive
        {
            get { return _isSectionInactive || !IsSectionActive; }
            set { this.RaiseAndSetIfChanged(ref _isSectionInactive, value); }
        }

        public string DescriptionText
        {
            get
            {
                if (IsInput)
                {
                    return "�K�{��������͂��Ă��������B";
                }
                else if (IsAggregated)
                {
                    return "����Ŏ���͏I���ł��B�����l�ł����B";
                }
                else
                {
                    return CurrentSection.Description;
                }
            }
        }

        private string _nextButtonText;
        public string NextButtonText
        {
            get
            {
                if (IsAggregated)
                {
                    return "�ۑ�";
                }
                else
                {
                    return "����";
                }
            }
            set { this.RaiseAndSetIfChanged(ref _nextButtonText, value); }
        }

        // Add a new property to hold the currently displayed questions
        public List<QuestionViewModel> DisplayedQuestionViewModels { get; set; }

        public bool AreAllDisplayedQuestionsAnswered()
        {
            // Check if all currently displayed questions are answered
            return DisplayedQuestionViewModels.All(q => q.IsAnswered);
        }
    }
}