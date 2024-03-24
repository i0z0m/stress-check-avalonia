using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace stress_check_avalonia
{
    public partial class NextButton : UserControl
    {
        public NextButton()
        {
            InitializeComponent();
        }

        public void ClickHandler(object sender, RoutedEventArgs args)
        {
            var mainWindow = this.FindAncestorOfType<MainWindow>();
            if (mainWindow != null && mainWindow.AreAllQuestionsAnswered())
            {
                int currentIndex = LoadSections.sections.IndexOf(SectionViewModel.Instance.CurrentSection);
                if (currentIndex < LoadSections.sections.Count - 1) // Check if it's not the last section
                {
                    // Load new section
                    mainWindow.InitSections(currentIndex + 1);
                }
            }
        }
    }
}
