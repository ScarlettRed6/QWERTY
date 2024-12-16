using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CIRCUIT.Model;
using CIRCUIT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CIRCUIT.ViewModel.CashierViewModel
{
    public class InventoryViewModel : ObservableObject
    {
        private ObservableCollection<ProductModel> _allProducts = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _pagedProducts;
        private ObservableCollection<ProductModel> _filteredProducts;
        private readonly Db _db;
        private string _searchText;

        private int _currentPage = 1;
        private int _itemsPerPage = 5;
        private int _totalItems;

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        public ObservableCollection<ProductModel> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
                UpdatePagedProducts();
            }
        }

        public ObservableCollection<ProductModel> PagedProducts
        {
            get => _pagedProducts;
            set
            {
                _pagedProducts = value;
                OnPropertyChanged(nameof(PagedProducts));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterProducts();
            }
        }

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

        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand FirstPageCommand { get; }
        public ICommand LastPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public InventoryViewModel()
        {
            _db = new Db();
            LoadProducts();
            SearchCommand = new RelayCommand(FilterProducts);
            FirstPageCommand = new RelayCommand(() => CurrentPage = 1);
            LastPageCommand = new RelayCommand(() => CurrentPage = TotalPages);
            NextPageCommand = new RelayCommand(() => CurrentPage++);
            PreviousPageCommand = new RelayCommand(() => CurrentPage--);
        }

        private void LoadProducts()
        {
            try
            {
                string query = "SELECT * FROM tbl_products WHERE is_archived = 0";
                var productsFromDb = _db.FetchData(query);

                _allProducts.Clear();

                foreach (var product in productsFromDb)
                {
                    _allProducts.Add(new ProductModel
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Category = product.Category,
                        Description = product.Description,
                        Brand = product.Brand,
                        ModelNumber = product.ModelNumber,
                        StockQuantity = product.StockQuantity,
                        UnitCost = product.UnitCost,
                        SellingPrice = product.SellingPrice,
                        MinStockLevel = product.MinStockLevel,
                        IsArchived = product.IsArchived,
                    });
                }

                FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);
            }
            else
            {
                FilteredProducts = new ObservableCollection<ProductModel>(
                    _allProducts.Where(p =>
                        p.ProductId.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        p.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        p.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }
        }

        private void UpdatePagedProducts()
        {
            if (FilteredProducts == null) return;

            TotalItems = FilteredProducts.Count;

            PagedProducts = new ObservableCollection<ProductModel>(
                FilteredProducts.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage)
            );
        }

    }
}