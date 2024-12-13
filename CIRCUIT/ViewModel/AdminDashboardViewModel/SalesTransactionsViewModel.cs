using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CIRCUIT.View.AdminDashboardViews;
using CIRCUIT.ViewModel.Bases;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class SalesTransactionsViewModel : BasePaginationViewModel
    {
        //Fields
        private int _itemsPerPage = 4; // Adjust number of items per page
        private string _searchTerm;
        private string _categoryBox;
        private string _timeFilter;
        SalesRepository saleConn = new SalesRepository();
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        private ViewSaleViewModel _saleViewModel;

        private object _currentView;
        private List<SaleModel> _items;
        private int _totalProductSold;
        private int _totalTransactions;

        //Collections
        public ObservableCollection<SaleModel> PagedSales { get; set; }
        public ObservableCollection<SaleModel> Sales { get; set; }

        //Commands
        public RelayCommand<SaleModel> ViewCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand CheckSelectAll { get; private set; }
        public RelayCommand CheckSelectCell { get; private set; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ApplyDateFilterCommand { get; }
        public RelayCommand FilterButtonCommand { get; }
        public RelayCommand ClearFilterFCommand { get; set; }

        //Properties 

        //For pagination
        //For pagination number of items per page
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                _itemsPerPage = value;
                OnPropertyChange();
                UpdatePagedItems();
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    OnPropertyChange();
                    UpdatePagedItems();
                }
            }
        }

        public string CategoryBox
        {
            get => _categoryBox;
            set
            {
                if (_categoryBox != value)
                {
                    _categoryBox = value;
                    OnPropertyChange();
                    ClearFilterFCommand.NotifyCanExecuteChanged();
                    //UpdatePagedSales();
                }
            }
        }

        public string TimeFilter
        {
            get => _timeFilter;
            set
            {
                if (_timeFilter != value)
                {
                    _timeFilter = value;
                    OnPropertyChange();
                    UpdatePagedItems();
                }
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChange();
                    ClearFilterFCommand.NotifyCanExecuteChanged();
                }
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChange();
                    ClearFilterFCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public int TotalTransactions 
        { 
            get => _totalTransactions; 
            set => _totalTransactions = value; 
        }

        public List<SaleModel> Items 
        { 
            get => _items; 
            set 
            { 
                _items = value;
                OnPropertyChange();
            }
        }

        public object CurrentView 
        { 
            get => _currentView; 
            set  
            { 
                _currentView = value;
                OnPropertyChange();
            }
        }

        public int TotalProductSold 
        { 
            get => _totalProductSold; 
            set 
            { 
                _totalProductSold = value; 
                OnPropertyChange();
            } 
        }

        //Constructor
        public SalesTransactionsViewModel()
        {
            Sales = new ObservableCollection<SaleModel>();
            PagedSales = new ObservableCollection<SaleModel>();
            FirstPageCommand = new RelayCommand(() => CurrentPage = 1);
            LastPageCommand = new RelayCommand(() => CurrentPage = TotalPages);
            NextPageCommand = new RelayCommand(() =>
            {
                if (CurrentPage < TotalPages)
                    CurrentPage++;
            });
            PreviousPageCommand = new RelayCommand(() =>
            {
                if (CurrentPage > 1)
                    CurrentPage--;
            });

            //CheckSelectAll = new RelayCommand(ChkSelectAllCommand);
            //CheckSelectCell = new RelayCommand(ChkSelectCellCommand);
            ViewCommand = new RelayCommand<SaleModel>(ExecuteViewSaleDetails);
            ExportCommand = new RelayCommand(ExportSelectedTransactions);
            ApplyDateFilterCommand = new RelayCommand(ApplyDateFilter);
            FilterButtonCommand = new RelayCommand(OpenWindowFilter);
            ClearFilterFCommand = new RelayCommand(ClearFilters, CanClearFilter);
            CategoryBox = "Payment Method";
            //IsAllSelected = false;

            UpdateList();

        }

        private bool CanClearFilter()
        {
           return !(string.IsNullOrWhiteSpace(CategoryBox) || CategoryBox == "Payment Method") 
                   || StartDate.HasValue 
                   || EndDate.HasValue;
        }

        private void ClearFilters()
        {
            CategoryBox = string.Empty;
            StartDate = null;   
            EndDate = null;
            UpdatePagedItems();
        }

        private void OpenWindowFilter()
        {
            SalesFilterWindow filterWindow = new SalesFilterWindow
            {
                DataContext = this // Share the same ViewModel
            };
            filterWindow.ShowDialog();
        }

        private void ApplyDateFilter()
        {
            UpdatePagedItems();
        }

        private void ExportSelectedTransactions()
        {
            // Use all transactions from PagedSales instead of filtering by selection
            var allTransactions = PagedSales.ToList();

            if (!allTransactions.Any())
            {
                MessageBox.Show("No transactions available for export.", "Export", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = "AllTransactionsExport.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string exportPath = saveFileDialog.FileName;
                PdfGenerator.GenerateSalesPdf(allTransactions, exportPath, saleConn);

                MessageBox.Show("Transactions exported successfully.", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void ExecuteViewSaleDetails(SaleModel? model)
        {
            _saleViewModel = new ViewSaleViewModel(model, this);
            CurrentView = _saleViewModel;
        }

        //Refreshes the products in the datagrid
        public void UpdateList()
        {
            var fetchedSales = saleConn.FetchSales();

            Sales.Clear();
            Items = new List<SaleModel>();
            foreach (var saleI in fetchedSales)
            {
                Sales.Add(saleI);
                Items.Add(saleI);
                TotalTransactions++;
            }

            TotalItems = Sales.Count;
            TotalProductSold = saleConn.FetchTotalProductSold();
            UpdatePagedItems();
        }

        //Updates data per page
        protected override void UpdatePagedItems()
        {
            IEnumerable<SaleModel> filteredItems = Sales;

            // For search term filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredItems = filteredItems.Where(p =>
                    p.CashierName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // For category filter
            if (!string.IsNullOrWhiteSpace(CategoryBox) && CategoryBox != "Payment Method")
            {
                filteredItems = filteredItems.Where(p => p.PaymentMethod.Equals(CategoryBox, StringComparison.OrdinalIgnoreCase));
            }

            // For time filter
            if (!string.IsNullOrWhiteSpace(TimeFilter))
            {
                var now = DateTime.Now;
                filteredItems = TimeFilter switch
                {
                    "Week" => filteredItems.Where(p => p.DateTime >= now.AddDays(-7)),
                    "Month" => filteredItems.Where(p => p.DateTime >= now.AddMonths(-1)),
                    "Year" => filteredItems.Where(p => p.DateTime >= now.AddYears(-1)),
                    _ => filteredItems
                };
            }

            if (StartDate.HasValue && EndDate.HasValue)
            {
                filteredItems = filteredItems.Where(p => p.DateTime >= StartDate.Value && p.DateTime <= EndDate.Value);
            }

            TotalItems = filteredItems.Count();
            PagedSales = new ObservableCollection<SaleModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChange(nameof(PagedSales));
        }

        //Search and filters data by Product name, filter can be modified later
        public void SearchItems(string searchTerm)
        {
            // Filter the product list based on the search term
            var filteredItems = Sales
                .Where(p => p.PaymentMethod.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            TotalItems = filteredItems.Count;
            PagedSales = new ObservableCollection<SaleModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );
            OnPropertyChange(nameof(PagedSales));
        }

    }
}
