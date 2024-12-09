using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class SalesTransactionsViewModel : ObservableObject
    {
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 4; // Adjust number of items per page
        private string _searchTerm;
        private string _categoryBox;
        private string _timeFilter;
        SalesRepository saleConn = new SalesRepository();
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        private ViewSaleViewModel _saleViewModel;

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

        //Properties
        [ObservableProperty]
        private object _currentView;

        //For total items in the dataset Product collection
        [ObservableProperty]
        private int _totalItems;

        //For listing selectedall
        [ObservableProperty]
        private List<SaleModel> _items;

        //Checks if checkbox for select all feature is true
        //[ObservableProperty]
        //private bool? _isAllSelected;

        [ObservableProperty]
        private int _totalProductSold;

        [ObservableProperty]
        private int _totalTransactions;

        //For pagination
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value && value > 0 && value <= TotalPages)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                    UpdatePagedSales();
                }
            }
        }
        //For pagination number of items per page
        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                _itemsPerPage = value;
                OnPropertyChanged();
                UpdatePagedSales();
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
                    OnPropertyChanged();
                    UpdatePagedSales();
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
                    OnPropertyChanged();
                    UpdatePagedSales();
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
                    OnPropertyChanged();
                    UpdatePagedSales();
                }
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

            //IsAllSelected = false;

            UpdateList();

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

        //Checks if all items are selected
        /* Might use this later for check box function
        private void ChkSelectAllCommand()
        {
            if (IsAllSelected == true)
            {
                Items.ForEach(x => x.IsSelected = true);
            }
            else if (IsAllSelected == false)
            {
                Items.ForEach(x => x.IsSelected = false);
            }

        }

        //Checks the selected cells
        private void ChkSelectCellCommand()
        {
            if (Items.All(x => x.IsSelected))
            {
                IsAllSelected = true;
            }
            else if (Items.All(x => !x.IsSelected))
            {
                IsAllSelected = false;
            }
            else
            {
                IsAllSelected = null;
            }

        }
        */

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
            UpdatePagedSales();
        }

        //Updates data per page
        private void UpdatePagedSales()
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

            TotalItems = filteredItems.Count();
            PagedSales = new ObservableCollection<SaleModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChanged(nameof(PagedSales));
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
            OnPropertyChanged(nameof(PagedSales));
        }

    }
}
