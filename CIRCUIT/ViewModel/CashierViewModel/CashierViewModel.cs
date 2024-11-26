using CIRCUIT.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace CIRCUIT.CashierViewModel
{
    public class CashierViewModel : PropertyChange
    {

        //GET TIME DATE 
        private string _currentDate;
        private string _currentTime;

        public CashierViewModel() 
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (sender, args) =>
            {
                CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
                CurrentTime = DateTime.Now.ToString("hh:mm tt");
            };
            timer.Start();

            CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
            CurrentTime = DateTime.Now.ToString("hh:mm tt");
        }

        // date

        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChange(nameof(CurrentDate));
            }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChange(nameof(CurrentTime));
            }
        }
        //ends here
    }
}
