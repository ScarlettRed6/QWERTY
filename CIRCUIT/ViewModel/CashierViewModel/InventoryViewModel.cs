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
        private ObservableCollection<ProductModel> _filteredProducts;
        private readonly Db _db;
        private string _searchText;

        public ObservableCollection<ProductModel> FilteredProducts
        {
            get => _filteredProducts;
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(nameof(FilteredProducts));
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

        public ICommand SearchCommand { get; }

        public InventoryViewModel()
        {
            _db = new Db();
            LoadProducts();
            SearchCommand = new RelayCommand(FilterProducts);
        }

        // Method to load products from the database
        private void LoadProducts()
        {
            try
            {
                // SQL query to fetch products that are not archived
                string query = "SELECT * FROM products WHERE is_archived = 0";

                // Fetch data from DB
                var productsFromDb = _db.FetchData(query);

                // Clear existing products in case we reload
                _allProducts.Clear();

                // Map fetched data into the ObservableCollection
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

                // Set the FilteredProducts to the collection of all products
                FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        // Method to filter products based on search text
        private void FilterProducts()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // If search text is empty, show all products
                FilteredProducts = new ObservableCollection<ProductModel>(_allProducts);
            }
            else
            {
                // Filter products by ProductId, ProductName, or Category (case-insensitive)
                FilteredProducts = new ObservableCollection<ProductModel>(
                    _allProducts.Where(p =>
                        p.ProductId.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        p.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                        p.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }
        }
    }
}