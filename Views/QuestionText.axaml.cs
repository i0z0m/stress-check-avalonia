using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace stress_check_avalonia
{
    public partial class QuestionText : UserControl
    {
        // Add AvaloniaProperty for QuestionIndex
        public static readonly AvaloniaProperty QuestionIndexProperty =
            AvaloniaProperty.Register<QuestionText, int>("QuestionIndex");

        public QuestionText()
        {
            InitializeComponent();
            DataContext = SectionViewModel.Instance;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Use AvaloniaProperty for QuestionIndex
        public int QuestionIndex
        {
            get { return (int)(GetValue(QuestionIndexProperty) ?? 0); } // Cast to int and handle possible null value
            set { SetValue(QuestionIndexProperty, value); }
        }

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