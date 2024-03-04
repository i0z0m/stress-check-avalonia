using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
{
    public partial class QuestionText : UserControl
    {
        public QuestionText()
        {
            InitializeComponent();
            DataContext = new SectionViewModel
            {
                Section = LoadSections.sections[0]
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (DataContext is SectionViewModel viewModel)
            {
                var firstQuestion = viewModel.Questions[0];
                // Convert the Id to string and display it
                var questionIdTextBlock = this.FindControl<TextBlock>("QuestionIdTextBlock");
                if (questionIdTextBlock != null)
                {
                    questionIdTextBlock.Text = firstQuestion.Id.ToString();
                }

                // Display the Text of the first question
                var questionTextTextBlock = this.FindControl<TextBlock>("QuestionTextTextBlock");
                if (questionTextTextBlock != null)
                {
                    questionTextTextBlock.Text = firstQuestion.Text;
                }
            }
        }
    }
}
