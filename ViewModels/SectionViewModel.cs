using System.Collections.Generic;
using System.ComponentModel;

namespace stress_check_avalonia
{
    public class SectionViewModel : INotifyPropertyChanged
    {
        private Section _section;

        public Section Section
        {
            get { return _section; }
            set
            {
                if (_section != value)
                {
                    _section = value;
                    OnPropertyChanged(nameof(Section));
                    OnPropertyChanged(nameof(Questions));
                }
            }
        }

        public List<Question> Questions => Section.Questions;
        public List<string> Choices => Section.Choices;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
