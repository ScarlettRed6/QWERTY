using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.SKCharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.IO;
using Axis = LiveChartsCore.SkiaSharpView.Axis;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public class HomeViewModel : PropertyChange, IDisposable
    {
        //Fields
        private decimal _grossProfit;
        private bool _disposed = false;
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

        //commands
        public RelayCommand ExportReportCommand { get; set; }

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
            ExportReportCommand = new RelayCommand(ExportReport);
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

            //the chart data
            var labels = new string[7];
            var values = new List<double>(new double[7]); 
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

            //the series , column series
            SeriesCollection = new ObservableCollection<ISeries>
            {
                new ColumnSeries<double>
                {
                    Values = values,
                    Fill = new SolidColorPaint(SKColors.Orange),
                }
            };

            //axes
            XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Labels = labels,
                    LabelsPaint = new SolidColorPaint(SKColors.Black), // Set axis text color
                    TextSize = 14
                }
            };

            YAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    MinLimit = values.Min() > 0 ? values.Min() : 0, // Avoid negative or invalid ranges
                    Labeler = value => value.ToString("N2"),
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
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
                    Fill = new SolidColorPaint(RandomColor()),  // Set random color
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
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
                    new SKColor(164, 164, 164)  
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
            var query = "SELECT * FROM tbl_products WHERE stock_quantity <= min_stock_level AND is_archived = 0";
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

        private byte[] CreateCartesianChartImage()
        {
            if (SeriesCollection == null || !SeriesCollection.Any())
            {
                throw new InvalidOperationException("SeriesCollection is empty or not initialized.");
            }

            var cartesianChart = new SKCartesianChart
            {
                Series = SeriesCollection,
                Width = 800,
                Height = 400,
                XAxes = XAxes?.ToArray(),
                YAxes = YAxes?.ToArray()
            };

            using var memoryStream = new MemoryStream();
            cartesianChart.SaveImage(memoryStream, SKEncodedImageFormat.Png, 100);
            return memoryStream.ToArray();
        }

        private byte[] CreatePieChartImage()
        {
            if (SeriesCollection2 == null || !SeriesCollection2.Any())
            {
                throw new InvalidOperationException("SeriesCollection2 is empty or not initialized.");
            }

            var pieChart = new SKPieChart
            {
                Series = SeriesCollection2,
                Width = 300,
                Height = 300,
                // Optionally, adjust size to make room for legend
                LegendPosition = LiveChartsCore.Measure.LegendPosition.Bottom
            };

            using var memoryStream = new MemoryStream();
            pieChart.SaveImage(memoryStream, SKEncodedImageFormat.Png, 100);
            return memoryStream.ToArray();
        }

        public string GetExportPath()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Export PDF",
                Filter = "PDF Files|*.pdf",
                FileName = "SalesReport.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            throw new InvalidOperationException("Export path not selected.");
        }

        public void ExportReport()
        {
            try
            {
                // Get the export path
                var exportPath = GetExportPath();

                // Generate chart images
                var cartesianChartImage = CreateCartesianChartImage();
                var pieChartImage = CreatePieChartImage();

                // Generate the PDF
                PdfGenerator.GenerateReport(
                    exportPath,
                    GrossProfit,
                    TotalSalesRevenue,
                    cartesianChartImage,
                    pieChartImage
                );

                Console.WriteLine("Report exported successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while exporting report: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    _dbCon?.Dispose();
                    availableColors?.Clear();
                }

                // Free unmanaged resources (if any) here.
                _disposed = true;
            }
        }

        // Destructor (finalizer) to ensure resources are released if Dispose is not called.
        ~HomeViewModel()
        {
            Dispose(false);
        }

    }
}
