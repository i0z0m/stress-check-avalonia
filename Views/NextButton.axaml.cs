using StressCheckAvalonia.ViewModels;
using StressCheckAvalonia.Models;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace StressCheckAvalonia.Views
{
    public partial class NextButton : UserControl
    {
        public NextButton()
        {
            InitializeComponent();
            DataContext = StateViewModel.Instance;
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            if (sender is Button)
            {
                (StateViewModel.Instance.CurrentState switch
                {
                    State.Input => new Action(() => StateViewModel.Instance.HandleInputState(true)),
                    State.SectionActive => new Action(() => StateViewModel.Instance.HandleSectionActiveState(true)),
                    State.Aggregated => new Action(() => Environment.Exit(0)),
                    _ => throw new InvalidOperationException("Undefined state for NextButton ClickHandler"),
                })();
            }
        }
    }
}