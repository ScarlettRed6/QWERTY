using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class HomeViewModel : PropertyChange
    {
        //Fields
        private decimal _grossProfit;
        private decimal _totalSalesRevenue;
        private readonly SalesRepository _salesRepository;
        private readonly Db _dbCon;

        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public string[] labels { get; set; }
        public ObservableCollection<SaleModel> Sales { get; set; }
        public ObservableCollection<ProductModel> Products { get; set; }

        public decimal GrossProfit
        {
            get 
            { 
                return _grossProfit; 
            }
            set 
            { 
                _grossProfit = value;
                OnPropertyChange();
            }
        }

        public decimal TotalSalesRevenue
        {
            get 
            { 
                return _totalSalesRevenue;
            }
            set 
            { 
                _totalSalesRevenue = value;
                OnPropertyChange();
            }
        }


        //Constructor
        public HomeViewModel()
        {
            _dbCon = new Db();
            _salesRepository = new SalesRepository();
            Products = new ObservableCollection<ProductModel>();
            Sales = new ObservableCollection<SaleModel>();
            InitializeCalculations();
            UpdateLatestTransaction();
            UpdateStockAlert();

        }

        //Method for barchart to display total revenues for the last 7 days , it shows revenues per day
        private void UpdateBarChart()
        {
            var revenues = _dbCon.FetchLast7DaysRevenue();

            var labels = new string[7];
            var values = new ChartValues<double>();
            DateTime today = DateTime.Today;

            for (int i = 0; i < 7; i++)
            {
                labels[i] = today.AddDays(-6 + i).ToString("dddd");
                values.Add(0); // Default revenue
            }

            //Map revenue data to the correct day in the 7 days
            foreach (var revenue in revenues)
            {
                int index = (revenue.SaleDate - today.AddDays(-6)).Days;
                if (index >= 0 && index < 7)
                {
                    values[index] = (double)revenue.TotalRevenue;
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = values,
                    Fill = Brushes.Red
                }
            };

            this.labels = labels;
            YFormatter = value => value.ToString("C");
            OnPropertyChange(nameof(SeriesCollection));
            OnPropertyChange(nameof(labels));
            OnPropertyChange(nameof(YFormatter));
        }

        private void UpdatePieChart()
        {
            var revenueData = _dbCon.FetchRevenuePerCategory();

            SeriesCollection2 = new SeriesCollection();

            foreach (var item in revenueData)
            {
                SeriesCollection2.Add(new PieSeries
                {
                    Title = item.Key, // Category name
                    Values = new ChartValues<double> { (double)item.Value }, // Revenue value
                    DataLabels = false,
                    LabelPoint = chartPoint => $"{chartPoint.Y} ({chartPoint.Participation:P})",
                    LabelPosition = PieLabelPosition.OutsideSlice
                });
            }

            OnPropertyChange(nameof(SeriesCollection2));
        }

        //Updates the latest transactions list
        private void UpdateLatestTransaction()
        {
            var fetchedSales = _salesRepository.FetchSales();
            //DateTime sevenDaysAgo = DateTime.Now.AddDays(-7); 
            DateTime currentTime = DateTime.Now.AddHours(-48);

            Sales.Clear();
            var recentSales = fetchedSales
                              .Where(sale => sale.DateTime >= currentTime)
                              .Take(4) 
                              .ToList();

            foreach (var sale in recentSales)
            {
                Sales.Add(sale);
            }

        }

        private void UpdateStockAlert()
        {
            var query = "SELECT * FROM products WHERE stock_quantity < 5 AND is_archived = 0";
            var fetchedProducts = _dbCon.FetchProducts(query);

            Products.Clear();
            foreach (var product in fetchedProducts)
            {
                Products.Add(product);
            }
        }

        //Gross pofit chart
        private void InitializeCalculations()
        {
            var result = _dbCon.FetchGrossProfit(DateTime.Now.AddYears(-1), DateTime.Now);
            GrossProfit = result.GrossProfit;
            TotalSalesRevenue = result.TotalRevenue;
            UpdatePieChart();
            UpdateBarChart();

        }


    }
}
