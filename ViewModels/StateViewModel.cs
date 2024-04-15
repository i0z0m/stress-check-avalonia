using ReactiveUI;
using StressCheckAvalonia.Models;
using System;

namespace StressCheckAvalonia.ViewModels
{
    public class StateViewModel : ReactiveObject
    {
        private static StateViewModel _instance;
        private State _currentState;

        public static StateViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateViewModel();
                }
                return _instance;
            }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentState, value);
                this.RaisePropertyChanged(nameof(IsInput));
                this.RaisePropertyChanged(nameof(IsSectionActive));
                this.RaisePropertyChanged(nameof(IsAggregated));
                this.RaisePropertyChanged(nameof(IsInputInverted));
                this.RaisePropertyChanged(nameof(AppTitle));
                this.RaisePropertyChanged(nameof(DescriptionText));
                this.RaisePropertyChanged(nameof(NextButtonText));
            }
        }

        public bool IsInput
        {
            get => CurrentState == State.Input;
        }
        public bool IsInputInverted => !IsInput;

        public bool IsSectionActive
        {
            get => CurrentState == State.SectionActive;
        }

        public bool IsAggregated
        {
            get => CurrentState == State.Aggregated;
        }

        public Section CurrentSection
        {
            get => SectionViewModel.Instance.CurrentSection;
            set
            {
                SectionViewModel.Instance.CurrentSection = value;
                this.RaisePropertyChanged(nameof(AppTitle));
            }
        }

        public string AppTitle
        {
            get
            {
                return CurrentState switch
                {
                    State.Input => "ストレスチェック開始",
                    State.SectionActive => $"STEP {CurrentSection.Step} {CurrentSection.Name}",
                    State.Aggregated => "ストレスチェック終了",
                    _ => throw new InvalidOperationException("Undefined state for AppTitle"),
                };
            }
        }

        public string DescriptionText
        {
            get
            {
                return CurrentState switch
                {
                    State.Input => "必須事項を入力してください。",
                    State.SectionActive => CurrentSection.Description,
                    State.Aggregated => "これで質問は、終わりです。お疲れさまでした。",
                    _ => throw new InvalidOperationException("Undefined state for DescriptionText"),
                };
            }
        }

        public string NextButtonText
        {
            get
            {
                return CurrentState switch
                {   
                    State.Input => "入力を完了",
                    State.SectionActive => "次へ",
                    State.Aggregated => "結果を保存",
                    _ => throw new InvalidOperationException("Undefined state for NextButtonText"),
                };
            }
        }
    }
}