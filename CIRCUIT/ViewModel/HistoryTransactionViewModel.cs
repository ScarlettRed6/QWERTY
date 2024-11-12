// HistoryTransactionViewModel.cs
using CIRCUIT.Model;
using System.ComponentModel;

namespace CIRCUIT.ViewModel
{
    public class HistoryTransactionViewModel : INotifyPropertyChanged
    {
        private readonly GetTimeandDate _getTimeandDate;
        private string _currentDateTime;

        public HistoryTransactionViewModel()
        {
            _getTimeandDate = new GetTimeandDate();
            _currentDateTime = _getTimeandDate.CurrentDateTime; 
        }

        public string CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    OnPropertyChanged(nameof(CurrentDateTime));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
