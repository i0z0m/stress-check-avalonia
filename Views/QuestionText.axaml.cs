using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
{
    public partial class QuestionText : UserControl
    {
        public QuestionText()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public int QuestionIndex { get; set; }
        public Question DisplayedQuestion { get; private set; }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (DataContext is SectionViewModel viewModel)
            {
                DisplayedQuestion = viewModel.Questions[QuestionIndex];
                viewModel.DisplayedQuestion = DisplayedQuestion;

            // Convert the Id to string and display it
            var questionIdTextBlock = this.FindControl<TextBlock>("QuestionIdTextBlock");
                if (questionIdTextBlock != null)
                {
                    questionIdTextBlock.Text = DisplayedQuestion.Id.ToString();
                }

                // Display the Text of the displayed question
                var questionTextTextBlock = this.FindControl<TextBlock>("QuestionTextTextBlock");
                if (questionTextTextBlock != null)
                {
                    questionTextTextBlock.Text = DisplayedQuestion.Text;
                }
            }
        }
    }
}