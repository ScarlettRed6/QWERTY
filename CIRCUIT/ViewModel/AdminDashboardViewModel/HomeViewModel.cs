using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Media;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using Axis = LiveChartsCore.SkiaSharpView.Axis;
using System.Windows;
using System;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class HomeViewModel : PropertyChange
    {
        //Fields
        private decimal _grossProfit;
        private decimal _totalSalesRevenue;
        private readonly SalesRepository _salesRepository;
        private readonly Db _dbCon;
        private List<SKColor> availableColors;
        private Random random;

        public ObservableCollection<ISeries> SeriesCollection { get; set; }
        public ObservableCollection<Axis> XAxes { get; set; }
        public ObservableCollection<Axis> YAxes { get; set; }
        public ISeries[] SeriesCollection2 { get; set; }
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
            availableColors = new List<SKColor>();
            random = new Random();
            InitializeCalculations();
            UpdateLatestTransaction();
            UpdateStockAlert();

        }

        //Method for barchart to display total revenues for the last 7 days , it shows revenues per day
        private void UpdateBarChart()
        {
            var revenues = _dbCon.FetchLast7DaysRevenue();

            // Prepare the chart data
            var labels = new string[7];
            var values = new List<double>(new double[7]); // Initial values as placeholder
            DateTime today = DateTime.Today;

            for (int i = 0; i < 7; i++)
            {
                labels[i] = today.AddDays(-6 + i).ToString("ddd");
            }

            foreach (var revenue in revenues)
            {
                int index = (revenue.SaleDate - today.AddDays(-6)).Days;
                if (index >= 0 && index < 7)
                {
                    values[index] = (double)revenue.TotalRevenue;
                }
            }

            // Define the series
            SeriesCollection = new ObservableCollection<ISeries>
            {
                new ColumnSeries<double>
                {
                    Values = values,
                    Fill = new SolidColorPaint(SKColors.Orange)
                }
            };

            // Define axes
            XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Labels = labels,
                    LabelsPaint = new SolidColorPaint(SKColors.White), // Set axis text color
                    TextSize = 14
                }
            };

            YAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    MinLimit = values.Min() > 0 ? values.Min() : 0, // Avoid negative or invalid ranges
                    Labeler = value => value.ToString("C"),
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 14
                }
            };

            OnPropertyChange(nameof(SeriesCollection));
            OnPropertyChange(nameof(XAxes));
            OnPropertyChange(nameof(YAxes));
        }

        private void UpdatePieChart()
        {
            var revenueData = _dbCon.FetchRevenuePerCategory();

            if (revenueData == null || !revenueData.Any())
            {
                Console.WriteLine("No data available for pie chart.");
                return;
            }

            // Create an array of ISeries
            var series = new List<ISeries>();

            foreach (var item in revenueData)
            {
                series.Add(new PieSeries<double>
                {
                    Name = item.Key,  // Category name
                    Values = new double[] { (double)item.Value },  // Format label
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,  // Position of the label
                    Fill = new SolidColorPaint(RandomColor())  // Set random color
                });
            }

            // Set the series collection to be bound to the PieChart
            SeriesCollection2 = series.ToArray();  // Convert the list to an array

            // Notify the UI that SeriesCollection2 has been updated
            OnPropertyChange(nameof(SeriesCollection2));
        }
        
        private SKColor RandomColor()
        {
            if (availableColors.Count == 0)
            {
                availableColors.AddRange(new List<SKColor>
                {
                    new SKColor(0, 0, 170),     
                    new SKColor(255, 140, 0),   
                    new SKColor(246, 246, 246)  
                });
            }

            var colorIndex = random.Next(availableColors.Count);
            var selectedColor = availableColors[colorIndex];
            availableColors.RemoveAt(colorIndex);

            return selectedColor;
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
