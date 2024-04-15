using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using StressCheckAvalonia.Models;
using System;

namespace StressCheckAvalonia.Views
{
    public partial class BackButtons : UserControl
    {
        public BackButtons()
        {
            InitializeComponent();
            DataContext = StateViewModel.Instance;
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            if (sender is Button button)
            {
                if (button.Name == "BackToTitleButton")
                {
                    StateViewModel.Instance.CurrentState = State.Input;
                }
                else if (button.Name == "BackOneScreenButton")
                {
                    (StateViewModel.Instance.CurrentState switch
                    {
                        State.SectionActive => new Action(() => StateViewModel.Instance.HandleSectionActiveState(false)),
                        State.Aggregated => new Action(() => StateViewModel.Instance.HandleAggregatedState(true)),
                        _ => throw new InvalidOperationException("Undefined state for BackButtons ClickHandler"),
                    })();
                }
            }
        }
    }
}