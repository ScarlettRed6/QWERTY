using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class StockSuppliersViewModel : ObservableObject
    {
        //Fields
        private int _currentPage = 1;
        private int _itemsPerPage = 5; // Adjust number of items per page
        private string _searchTerm;
        private StockControlRepository _sControlRepo;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        private ViewSaleViewModel _saleViewModel;

        //Collections
        public ObservableCollection<SuppliersModel> PagedSuppliers { get; set; }
        public ObservableCollection<SuppliersModel> Suppliers { get; set; }

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
        private List<SuppliersModel> _items;

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

        //Constructor
        public StockSuppliersViewModel()
        {
            _sControlRepo = new StockControlRepository();
            Suppliers = new ObservableCollection<SuppliersModel>();
            PagedSuppliers = new ObservableCollection<SuppliersModel>();
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

            UpdateList();

        }

        //Refreshes the products in the datagrid
        public void UpdateList()
        {
            string query = "SELECT * FROM tbl_suppliers";

            var fetchedSales = _sControlRepo.FetchSuppliers(query);

            Suppliers.Clear();
            Items = new List<SuppliersModel>();
            foreach (var saleI in fetchedSales)
            {
                Suppliers.Add(saleI);
                Items.Add(saleI);
                TotalTransactions++;
            }

            TotalItems = Suppliers.Count;
            UpdatePagedSales();
        }

        //Updates data per page
        private void UpdatePagedSales()
        {
            IEnumerable<SuppliersModel> filteredItems = Suppliers;

            // For search term filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredItems = filteredItems.Where(p =>
                    p.ContactName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            TotalItems = filteredItems.Count();
            PagedSuppliers = new ObservableCollection<SuppliersModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChanged(nameof(PagedSuppliers));
        }

        //Search and filters data by Product name, filter can be modified later
        public void SearchItems(string searchTerm)
        {
            // Filter the product list based on the search term
            var filteredItems = Suppliers
                .Where(p => p.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            TotalItems = filteredItems.Count;
            PagedSuppliers = new ObservableCollection<SuppliersModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );
            OnPropertyChanged(nameof(PagedSuppliers));
        }

    }
}
