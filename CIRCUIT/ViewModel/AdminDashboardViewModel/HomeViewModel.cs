using CIRCUIT.Utilities;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class HomeViewModel : PropertyChange
    {
      
        //Test
        //public ICommand btnTestCommand { get; }
        //Test

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] labels { get; set; }
        //public ObservableCollection<double> _data; 

        public HomeViewModel()
        {

            //_data = new ObservableCollection<double> { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};
            SalesPerDay();
            GrossProfit();
            //btnTestCommand = new CommandRelay(ExecuteTest); Part of the testButton, remove or use later

        }

        /* This is for testing the graphs, remove later or use later
        private void ExecuteTest(object obj)
        {
            if (SeriesCollection[0].Values.Count >= 7)
            {
                SeriesCollection[0].Values.RemoveAt(0);
            }
            SeriesCollection[0].Values.Add(48d);
            
        }*/

        //Sales per day chart
        private void SalesPerDay()
        {
            double Sunday = 123.53;
            double Monday = 154.73;
            double Tuesday = 215.62;
            double Wednesday = 153.86;
            double Thursday = 674.60;
            double Friday = 1256.90;
            double Saturday = 174.78;

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sales",
                    Values = new ChartValues<double> {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday},
                    Fill = Brushes.Red,


                }

            };

            labels = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            YFormatter = value => value.ToString("C");
        }

        //Gross pofit chart
        private void GrossProfit()
        {
            
        }

    }
}
