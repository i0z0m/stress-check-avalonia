using StressCheckAvalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
                // Convert the Id to string and display it
                var questionIdTextBlock = this.FindControl<TextBlock>("QuestionIdTextBlock");
                if (questionIdTextBlock != null)
                {
                    questionIdTextBlock.Text = viewModel.Question.Id.ToString();
                }

                // Display the Text of the displayed question
                var questionTextTextBlock = this.FindControl<TextBlock>("QuestionTextTextBlock");
                if (questionTextTextBlock != null)
                {
                    questionTextTextBlock.Text = viewModel.Question.Text;
                }
            }
        }
    }
}