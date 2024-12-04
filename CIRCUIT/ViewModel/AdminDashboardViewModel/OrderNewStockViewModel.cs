using CIRCUIT.Model;
using CIRCUIT.Model.DataRepositories;
using CIRCUIT.Utilities;
using CIRCUIT.View.AdminDashboardViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;

namespace CIRCUIT.ViewModel.AdminDashboardViewModel
{
    public partial class OrderNewStockViewModel : ObservableObject
    {
        //Fields and properties
        private int _currentPage = 1;
        private int _itemsPerPage = 9; // Adjust number of items per page
        private string _searchTerm;
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        private StockControlRepository _sControlRepo;
        private Db _dbCon;

        //Collections 
        public ObservableCollection<ProductModel> ProductsForOrder { get; set; }
        public ObservableCollection<ProductModel> Products { get; set; }

        // Collection for ComboBox items
        [ObservableProperty]
        private ObservableCollection<string> _suppliers;

        // Selected supplier
        [ObservableProperty]
        private string _selectedSupplier;

        //For total items in the dataset Product collection
        [ObservableProperty]
        private int _totalItems;

        //Commands
        public RelayCommand OpenReviewOrderCommand { get; set; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }

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
                    UpdatePagedProducts();
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
                UpdatePagedProducts();
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
                    UpdatePagedProducts();
                }
            }
        }

        //Constructor
        public OrderNewStockViewModel()
        {
            _sControlRepo = new StockControlRepository();
            _dbCon = new Db();
            Products = new ObservableCollection<ProductModel>();
            ProductsForOrder = new ObservableCollection<ProductModel>();
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

            OpenReviewOrderCommand = new RelayCommand(OpenReviewOrder);

            LoadComboboxSuppliers();
            LoadProducts();

        }

        //Openreview modal window method command
        private void OpenReviewOrder()
        {
            // Filter products with OrderQuantity > 0
            var filteredProducts = ProductsForOrder.Where(p => p.OrderQuantity > 0).ToList();

            if (!filteredProducts.Any() || string.IsNullOrEmpty(SelectedSupplier))
            {
                MessageBox.Show("No items selected for order!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Open the ReviewOrderWindow and pass the filtered products
            var reviewOrderWindow = new ReviewOrderWindow(new ObservableCollection<ProductModel>(filteredProducts), SelectedSupplier);
            reviewOrderWindow.ShowDialog();

        }

        //Method to load the comboboxt items
        private void LoadComboboxSuppliers()
        {
            var name = _sControlRepo.FetchSupplierNames();
            Suppliers = new ObservableCollection<string>(name);
        }

        private void LoadProducts()
        {
            string query = "SELECT * FROM products WHERE is_archived = 0";
            var products = _dbCon.FetchProducts(query);

            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
            UpdatePagedProducts();
        }

        //Updates data per page
        private void UpdatePagedProducts()
        {
            IEnumerable<ProductModel> filteredItems = Products;

            // For search term filter
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredItems = filteredItems.Where(p =>
                    p.ProductName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            TotalItems = filteredItems.Count();
            ProductsForOrder = new ObservableCollection<ProductModel>(
                filteredItems.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );

            OnPropertyChanged(nameof(ProductsForOrder));
        }


    }
}
