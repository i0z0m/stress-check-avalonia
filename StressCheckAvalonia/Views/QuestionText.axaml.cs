using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Globalization; // Ensure this namespace is included for CultureInfo

namespace StressCheckAvalonia.Views
{
    public partial class QuestionText : UserControl
    {
        public QuestionText()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            base.OnDataContextChanged(e);
            UpdateDisplayedQuestion();
        }

        private void UpdateDisplayedQuestion()
        {
            if (DataContext is QuestionViewModel viewModel)
            {
                var questionIdTextBlock = this.FindControl<TextBlock>("QuestionIdTextBlock");
                if (questionIdTextBlock != null)
                {
                    // Use CultureInfo.InvariantCulture to ensure consistent behavior across locales
                    questionIdTextBlock.Text = viewModel.Question.Id.ToString(CultureInfo.InvariantCulture);
                }

                var questionTextTextBlock = this.FindControl<TextBlock>("QuestionTextTextBlock");
                if (questionTextTextBlock != null)
                {
                    questionTextTextBlock.Text = viewModel.Question.Text;
                }
            }
        }
    }
}